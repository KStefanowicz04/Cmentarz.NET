using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektCmentarz.Data;
using ProjektCmentarz.Models;
using X.PagedList.Extensions;

namespace ProjektCmentarz.Controllers
{
    public class GraveyardSectionsController : Controller
    {
        private readonly GraveyardContext _context;

        public GraveyardSectionsController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: GraveyardSections
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sections.ToListAsync());
        }

        // GET: GraveyardSections/Details/5
        public async Task<IActionResult> Details(int? id, int? page)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Sekcja o danym Id
            var graveyardSection = await _context.Sections
                .FirstOrDefaultAsync(s => s.Id == id);

            if (graveyardSection == null)
            {
                return NotFound();
            }


            // Stronicowanie działek
            int pageSize = 16;  // Liczba rekordów na stronę
            int pageNumber = page ?? 1;  // Numer obecnej strony

            // Działki mające właściciela
            var ownedPlots = _context.Plots
                .Where(p => p.GraveyardSectionId == id && p.PlotOwnerId != null)
                .Include(p => p.Owner)
                .OrderBy(p => p.Id)
                .ToPagedList(pageNumber, pageSize);

            ViewBag.Plots = ownedPlots;

            return View(graveyardSection);
        }

    }
}
