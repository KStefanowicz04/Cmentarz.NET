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
    public class CasketsController : Controller
    {
        private readonly GraveyardContext _context;

        public CasketsController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: Caskets
        public async Task<IActionResult> Index()
        {
            var graveyardContext = _context.Casket.Include(c => c.Deceased).Include(c => c.Material);
            return View(await graveyardContext.ToListAsync());
        }

        // GET: Caskets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var casket = await _context.Casket
                .Include(c => c.Deceased)
                .Include(c => c.Material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (casket == null)
            {
                return NotFound();
            }

            return View(casket);
        }

        // GET: Caskets/Create
        public IActionResult Create()
        {
            ViewData["DeceasedId"] = new SelectList(_context.Deceaseds, "Id", "FirstName");
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Type");
            return View();
        }

        // POST: Caskets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MaterialId,Price,DeceasedId")] Casket casket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(casket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeceasedId"] = new SelectList(_context.Deceaseds, "Id", "FirstName", casket.DeceasedId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Type", casket.MaterialId);
            return View(casket);
        }

        // GET: Caskets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var casket = await _context.Casket.FindAsync(id);
            if (casket == null)
            {
                return NotFound();
            }
            ViewData["DeceasedId"] = new SelectList(_context.Deceaseds, "Id", "FirstName", casket.DeceasedId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Type", casket.MaterialId);
            return View(casket);
        }

        // POST: Caskets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MaterialId,Price,DeceasedId")] Casket casket)
        {
            if (id != casket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(casket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CasketExists(casket.Id))
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
            ViewData["DeceasedId"] = new SelectList(_context.Deceaseds, "Id", "FirstName", casket.DeceasedId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Type", casket.MaterialId);
            return View(casket);
        }

        // GET: Caskets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var casket = await _context.Casket
                .Include(c => c.Deceased)
                .Include(c => c.Material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (casket == null)
            {
                return NotFound();
            }

            return View(casket);
        }

        // POST: Caskets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var casket = await _context.Casket.FindAsync(id);
            if (casket != null)
            {
                _context.Casket.Remove(casket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CasketExists(int id)
        {
            return _context.Casket.Any(e => e.Id == id);
        }
    }
}
