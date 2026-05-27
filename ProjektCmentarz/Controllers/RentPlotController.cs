using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ProjektCmentarz.Data;
using ProjektCmentarz.Models;
using X.PagedList;

namespace ProjektCmentarz.Controllers
{
    public class RentPlotController : Controller
    {
        private readonly GraveyardContext _context;

        public RentPlotController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: RentPlot
        // Strona dostępna tylko dla zalogowanych
        [Authorize]
        public async Task<IActionResult> Index(string searchString, int? sectionId, int? page)
        {
            // Sekcje do dropdownu wyszukiwania
            ViewBag.Sections = await _context.GraveyardSection
                .OrderBy(gs => gs.Id)
                .ToListAsync();

            // Ta strona wyświetla tylko działki wolne, czyli takie które nie mają właściciela ani żadnych grobów.
            var freePlotsQuery = _context.Plots
                .Include(p => p.Graves)
                .Include(p => p.Owner)
                .Include(p => p.GraveyardSection)
                .Where(p => p.Owner == null && (p.Graves == null || !p.Graves.Any()))
                .AsQueryable();

            // Filtrowanie po sekcji cmentarza wybranej z dropdownu
            if (sectionId.HasValue)
            {
                freePlotsQuery = freePlotsQuery.Where(p => p.GraveyardSectionId == sectionId.Value);
            }

            // Stronicowanie
            int pageSize = 16;  // Liczba rekordów na stronę
            int pageNumber = page ?? 1;  // Numer obecnej strony

            // Asynchroniczne pobranie danych z bazy
            var items = await freePlotsQuery
                .OrderBy(p => p.GraveyardSectionId)
                .ToListAsync();

            var pagedList = new PagedList<Plot>(items, pageNumber, pageSize);

            return View(pagedList);
        }


        // GET: RentPlot/Details/5
        public async Task<IActionResult> Details(int id)
        {
            // Podstrona Details przedstawiająca informacje o danej działce
            var plot = await _context.Plots
                .Include(p => p.GraveyardSection)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (plot == null)
                return NotFound();

            return View(plot);
        }

        // Wybiera id Działki i PlotOwner dla danego Użytkownika
        // POST: RentPlot/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm(int id)
        {
            // ID Użytkownika który chcec wynająć daną działkę jest pobierane z Claimu UserID przypisanego przy logowaniu
            int userId = int.Parse(User.FindFirst("UserId").Value);

            // Wybieramy użytkownika z bazy danych o tym samym ID co zalogowany użytkownik
            var user = await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
                return NotFound();


            // Próba znalezienie rekordu PlotOwner danego użytkownika
            var owner = _context.PlotOwners.FirstOrDefault(o => o.UserId == userId);
            // Jeśli dany użytkownik nie ma swojego PlotOwner, zostanie on utworzony
            if (owner == null)
            {
                owner = new PlotOwner
                {
                    FirstName = user.FirstName,
                    Surname = user.Surname,
                    UserId = userId,
                    ContactDataId = user.ContactDataId
                };

                _context.PlotOwners.Add(owner);
                await _context.SaveChangesAsync();
            }



            // Przypisanie Id Właściciela do jego Działki
            var plot = await _context.Plots.FindAsync(id);
            if (plot == null)
                return NotFound();

            // Utworzenie nowej płatności; zostanie usunięta z bazy jeśli płatność przez Stripe nie powiedzie się.
            var payment = new Payment
            {
                PlotId = plot.Id,
                PlotOwnerId = owner.Id,
                Price = plot.PlotValue,
                PaymentDate = null
            };
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            // Opłatą zajmuje się PaymentController
            return RedirectToAction("Pay", "Payments", new { id = payment.Id });
        }

    }
}
