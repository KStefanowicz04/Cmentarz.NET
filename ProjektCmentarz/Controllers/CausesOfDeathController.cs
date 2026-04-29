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
    public class CausesOfDeathController : Controller
    {
        private readonly GraveyardContext _context;

        public CausesOfDeathController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: CausesOfDeath
        public async Task<IActionResult> Index()
        {
            return View(await _context.CausesOfDeath.ToListAsync());
        }

        // GET: CausesOfDeath/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var causesOfDeath = await _context.CausesOfDeath
                .FirstOrDefaultAsync(m => m.Id == id);
            if (causesOfDeath == null)
            {
                return NotFound();
            }

            return View(causesOfDeath);
        }

        // GET: CausesOfDeath/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CausesOfDeath/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cause")] CauseOfDeath causesOfDeath)
        {
            if (ModelState.IsValid)
            {
                _context.Add(causesOfDeath);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(causesOfDeath);
        }

        // GET: CausesOfDeath/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var causesOfDeath = await _context.CausesOfDeath.FindAsync(id);
            if (causesOfDeath == null)
            {
                return NotFound();
            }
            return View(causesOfDeath);
        }

        // POST: CausesOfDeath/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cause")] CauseOfDeath causesOfDeath)
        {
            if (id != causesOfDeath.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(causesOfDeath);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CauseOfDeathExists(causesOfDeath.Id))
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
            return View(causesOfDeath);
        }

        // GET: CausesOfDeath/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var causesOfDeath = await _context.CausesOfDeath
                .FirstOrDefaultAsync(m => m.Id == id);
            if (causesOfDeath == null)
            {
                return NotFound();
            }

            return View(causesOfDeath);
        }

        // POST: CausesOfDeath/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var causesOfDeath = await _context.CausesOfDeath.FindAsync(id);
            if (causesOfDeath != null)
            {
                _context.CausesOfDeath.Remove(causesOfDeath);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CauseOfDeathExists(int id)
        {
            return _context.CausesOfDeath.Any(e => e.Id == id);
        }
    }
}
