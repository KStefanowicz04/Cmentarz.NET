using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektCmentarz.Data;
using ProjektCmentarz.Models;

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
        public async Task<IActionResult> Index()
        {
            // Ta strona wyświetla tylko działki wolne, czyli takie które nie mają właściciela ani żadnych grobów.
            var freePlots = await _context.Plots
                .Include(p => p.Graves)
                .Include(p => p.Owner)
                .Include(p => p.GraveyardSection)
                .Where(p => p.Owner == null && (p.Graves == null || !p.Graves.Any()))
                .ToListAsync();

            return View(freePlots);
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
                _context.SaveChangesAsync();
            }

            // Przypisanie Id Właściciela do jego Działki
            var plot = await _context.Plots.FindAsync(id);
            plot.PlotOwnerId = owner.Id;
            _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
