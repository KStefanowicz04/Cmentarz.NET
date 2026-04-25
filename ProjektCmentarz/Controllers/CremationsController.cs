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
    public class CremationsController : Controller
    {
        private readonly GraveyardContext _context;

        public CremationsController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: Cremations
        public async Task<IActionResult> Index()
        {
            var graveyardContext = _context.Cremations.Include(c => c.Deceased);
            return View(await graveyardContext.ToListAsync());
        }

        // GET: Cremations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cremation = await _context.Cremations
                .Include(c => c.Deceased)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cremation == null)
            {
                return NotFound();
            }

            return View(cremation);
        }

        // GET: Cremations/Create
        public IActionResult Create()
        {
            ViewData["DeceasedId"] = new SelectList(_context.Deceaseds, "Id", "FirstName");
            return View();
        }

        // POST: Cremations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CremationDate,DeceasedId")] Cremation cremation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cremation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeceasedId"] = new SelectList(_context.Deceaseds, "Id", "FirstName", cremation.DeceasedId);
            return View(cremation);
        }

        // GET: Cremations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cremation = await _context.Cremations.FindAsync(id);
            if (cremation == null)
            {
                return NotFound();
            }
            ViewData["DeceasedId"] = new SelectList(_context.Deceaseds, "Id", "FirstName", cremation.DeceasedId);
            return View(cremation);
        }

        // POST: Cremations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CremationDate,DeceasedId")] Cremation cremation)
        {
            if (id != cremation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cremation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CremationExists(cremation.Id))
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
            ViewData["DeceasedId"] = new SelectList(_context.Deceaseds, "Id", "FirstName", cremation.DeceasedId);
            return View(cremation);
        }

        // GET: Cremations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cremation = await _context.Cremations
                .Include(c => c.Deceased)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cremation == null)
            {
                return NotFound();
            }

            return View(cremation);
        }

        // POST: Cremations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cremation = await _context.Cremations.FindAsync(id);
            if (cremation != null)
            {
                _context.Cremations.Remove(cremation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CremationExists(int id)
        {
            return _context.Cremations.Any(e => e.Id == id);
        }
    }
}
