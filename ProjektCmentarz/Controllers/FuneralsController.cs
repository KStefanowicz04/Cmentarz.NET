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
    public class FuneralsController : Controller
    {
        private readonly GraveyardContext _context;

        public FuneralsController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: Funerals
        public async Task<IActionResult> Index()
        {
            var graveyardContext = _context.Funerals.Include(f => f.Deceased).Include(f => f.FuneralPlot).Include(f => f.FuneralPriest);
            return View(await graveyardContext.ToListAsync());
        }

        // GET: Funerals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funeral = await _context.Funerals
                .Include(f => f.Deceased)
                .Include(f => f.FuneralPlot)
                .Include(f => f.FuneralPriest)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funeral == null)
            {
                return NotFound();
            }

            return View(funeral);
        }

        // GET: Funerals/Create
        public IActionResult Create()
        {
            ViewData["DeceasedId"] = new SelectList(_context.Deceaseds, "Id", "FirstName");
            ViewData["PlotId"] = new SelectList(_context.Plots, "Id", "Id");
            ViewData["PriestId"] = new SelectList(_context.Priests, "Id", "FirstName");
            return View();
        }

        // POST: Funerals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FuneralDate,PlotId,DeceasedId,PriestId")] Funeral funeral)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funeral);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeceasedId"] = new SelectList(_context.Deceaseds, "Id", "FirstName", funeral.DeceasedId);
            ViewData["PlotId"] = new SelectList(_context.Plots, "Id", "Id", funeral.PlotId);
            ViewData["PriestId"] = new SelectList(_context.Priests, "Id", "FirstName", funeral.PriestId);
            return View(funeral);
        }

        // GET: Funerals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funeral = await _context.Funerals.FindAsync(id);
            if (funeral == null)
            {
                return NotFound();
            }
            ViewData["DeceasedId"] = new SelectList(_context.Deceaseds, "Id", "FirstName", funeral.DeceasedId);
            ViewData["PlotId"] = new SelectList(_context.Plots, "Id", "Id", funeral.PlotId);
            ViewData["PriestId"] = new SelectList(_context.Priests, "Id", "FirstName", funeral.PriestId);
            return View(funeral);
        }

        // POST: Funerals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FuneralDate,PlotId,DeceasedId,PriestId")] Funeral funeral)
        {
            if (id != funeral.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funeral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuneralExists(funeral.Id))
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
            ViewData["DeceasedId"] = new SelectList(_context.Deceaseds, "Id", "FirstName", funeral.DeceasedId);
            ViewData["PlotId"] = new SelectList(_context.Plots, "Id", "Id", funeral.PlotId);
            ViewData["PriestId"] = new SelectList(_context.Priests, "Id", "FirstName", funeral.PriestId);
            return View(funeral);
        }

        // GET: Funerals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funeral = await _context.Funerals
                .Include(f => f.Deceased)
                .Include(f => f.FuneralPlot)
                .Include(f => f.FuneralPriest)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funeral == null)
            {
                return NotFound();
            }

            return View(funeral);
        }

        // POST: Funerals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funeral = await _context.Funerals.FindAsync(id);
            if (funeral != null)
            {
                _context.Funerals.Remove(funeral);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuneralExists(int id)
        {
            return _context.Funerals.Any(e => e.Id == id);
        }
    }
}
