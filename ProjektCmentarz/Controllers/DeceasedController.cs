using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektCmentarz.Data;
using ProjektCmentarz.Models;

namespace ProjektCmentarz.Controllers
{
    public class DeceasedController : Controller
    {
        private readonly GraveyardContext _context;

        public DeceasedController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: Deceased
        public async Task<IActionResult> Index(string searchString, DateTime? searchDate, DateTime? searchDeathDate)
        {
            var deceasedQuery = from d in _context.Deceaseds
                                select d;

            // Filtrowanie tekstowe (Imię lub Nazwisko)
            if (!string.IsNullOrEmpty(searchString))
            {
                deceasedQuery = deceasedQuery.Where(s => s.FirstName.Contains(searchString) || s.Surname.Contains(searchString));
            }

            // Filtrowanie po dacie urodzenia
            if (searchDate.HasValue)
            {
                deceasedQuery = deceasedQuery.Where(s => s.BirthDate.Date == searchDate.Value.Date);
            }

            // Filtrowanie po dacie zgonu
            if (searchDeathDate.HasValue)
            {
                deceasedQuery = deceasedQuery.Where(s => s.DeathDate.Date == searchDeathDate.Value.Date);
            }

            return View(await deceasedQuery.ToListAsync());
        }

        // GET: Deceased/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var deceased = await _context.Deceaseds.FirstOrDefaultAsync(m => m.Id == id);

            if (deceased == null) return NotFound();

            return View(deceased);
        }

        // --- TYLKO DLA ADMINA ---

        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,FirstName,Surname,BirthDate,DeathDate,CasketId,FuneralId")] Deceased deceased)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deceased);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deceased);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var deceased = await _context.Deceaseds.FindAsync(id);
            if (deceased == null) return NotFound();

            return View(deceased);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,Surname,BirthDate,DeathDate,CasketId,FuneralId")] Deceased deceased)
        {
            if (id != deceased.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deceased);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeceasedExists(deceased.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(deceased);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var deceased = await _context.Deceaseds.FirstOrDefaultAsync(m => m.Id == id);
            if (deceased == null) return NotFound();

            return View(deceased);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deceased = await _context.Deceaseds.FindAsync(id);
            if (deceased != null)
            {
                _context.Deceaseds.Remove(deceased);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DeceasedExists(int id) => _context.Deceaseds.Any(e => e.Id == id);
    }
}