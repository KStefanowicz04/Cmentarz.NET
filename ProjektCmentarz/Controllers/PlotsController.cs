using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektCmentarz.Data;
using ProjektCmentarz.Models;

namespace ProjektCmentarz.Controllers
{
    public class PlotsController : Controller
    {
        private readonly GraveyardContext _context;

        public PlotsController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: Plots
        public async Task<IActionResult> Index()
        {
            var graveyardContext = _context.Plots.Include(p => p.GraveyardSection).Include(p => p.Owner);
            return View(await graveyardContext.ToListAsync());
        }

        // GET: Plots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plot = await _context.Plots
                .Include(p => p.GraveyardSection)
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plot == null)
            {
                return NotFound();
            }

            return View(plot);
        }

        // GET: Plots/Create
        public IActionResult Create()
        {
            ViewData["GraveyardSectionId"] = new SelectList(_context.Sections, "Id", "SectionType");
            ViewData["PlotOwnerId"] = new SelectList(_context.PlotOwners, "Id", "FirstName");
            return View();
        }

        // POST: Plots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlotOwnerId,GraveyardSectionId")] Plot plot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GraveyardSectionId"] = new SelectList(_context.Sections, "Id", "SectionType", plot.GraveyardSectionId);
            ViewData["PlotOwnerId"] = new SelectList(_context.PlotOwners, "Id", "FirstName", plot.PlotOwnerId);
            return View(plot);
        }

        // GET: Plots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plot = await _context.Plots.FindAsync(id);
            if (plot == null)
            {
                return NotFound();
            }
            ViewData["GraveyardSectionId"] = new SelectList(_context.Sections, "Id", "SectionType", plot.GraveyardSectionId);
            ViewData["PlotOwnerId"] = new SelectList(_context.PlotOwners, "Id", "FirstName", plot.PlotOwnerId);
            return View(plot);
        }

        // POST: Plots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlotOwnerId,GraveyardSectionId")] Plot plot)
        {
            if (id != plot.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlotExists(plot.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GraveyardSectionId"] = new SelectList(_context.Sections, "Id", "SectionType", plot.GraveyardSectionId);
            ViewData["PlotOwnerId"] = new SelectList(_context.PlotOwners, "Id", "FirstName", plot.PlotOwnerId);
            return View(plot);
        }

        // GET: Plots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plot = await _context.Plots
                .Include(p => p.GraveyardSection)
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plot == null)
            {
                return NotFound();
            }

            return View(plot);
        }

        // POST: Plots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plot = await _context.Plots.FindAsync(id);
            if (plot != null)
            {
                _context.Plots.Remove(plot);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlotExists(int id)
        {
            return _context.Plots.Any(e => e.Id == id);
        }
    }
}
