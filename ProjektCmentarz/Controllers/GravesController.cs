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
    public class GravesController : Controller
    {
        private readonly GraveyardContext _context;

        public GravesController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: Graves
        public async Task<IActionResult> Index()
        {
            var graveyardContext = _context.Graves.Include(g => g.BurialDepth).Include(g => g.GraveDeceased).Include(g => g.GravePlot);
            return View(await graveyardContext.ToListAsync());
        }

        // GET: Graves/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grave = await _context.Graves
                .Include(g => g.BurialDepth)
                .Include(g => g.GraveDeceased)
                .Include(g => g.GravePlot)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grave == null)
            {
                return NotFound();
            }

            return View(grave);
        }

        // GET: Graves/Create
        public IActionResult Create()
        {
            ViewData["BurialDepthId"] = new SelectList(_context.Set<BurialDepth>(), "Id", "Depth");
            ViewData["DeceasedId"] = new SelectList(_context.Deceaseds, "Id", "FirstName");
            ViewData["PlotId"] = new SelectList(_context.Plots, "Id", "Id");
            return View();
        }

        // POST: Graves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlotId,DeceasedId,BurialDepthId")] Grave grave)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grave);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BurialDepthId"] = new SelectList(_context.Set<BurialDepth>(), "Id", "Depth", grave.BurialDepthId);
            ViewData["DeceasedId"] = new SelectList(_context.Deceaseds, "Id", "FirstName", grave.DeceasedId);
            ViewData["PlotId"] = new SelectList(_context.Plots, "Id", "Id", grave.PlotId);
            return View(grave);
        }

        // GET: Graves/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grave = await _context.Graves.FindAsync(id);
            if (grave == null)
            {
                return NotFound();
            }
            ViewData["BurialDepthId"] = new SelectList(_context.Set<BurialDepth>(), "Id", "Depth", grave.BurialDepthId);
            ViewData["DeceasedId"] = new SelectList(_context.Deceaseds, "Id", "FirstName", grave.DeceasedId);
            ViewData["PlotId"] = new SelectList(_context.Plots, "Id", "Id", grave.PlotId);
            return View(grave);
        }

        // POST: Graves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlotId,DeceasedId,BurialDepthId")] Grave grave)
        {
            if (id != grave.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grave);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GraveExists(grave.Id))
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
            ViewData["BurialDepthId"] = new SelectList(_context.Set<BurialDepth>(), "Id", "Depth", grave.BurialDepthId);
            ViewData["DeceasedId"] = new SelectList(_context.Deceaseds, "Id", "FirstName", grave.DeceasedId);
            ViewData["PlotId"] = new SelectList(_context.Plots, "Id", "Id", grave.PlotId);
            return View(grave);
        }

        // GET: Graves/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grave = await _context.Graves
                .Include(g => g.BurialDepth)
                .Include(g => g.GraveDeceased)
                .Include(g => g.GravePlot)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grave == null)
            {
                return NotFound();
            }

            return View(grave);
        }

        // POST: Graves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grave = await _context.Graves.FindAsync(id);
            if (grave != null)
            {
                _context.Graves.Remove(grave);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GraveExists(int id)
        {
            return _context.Graves.Any(e => e.Id == id);
        }
    }
}
