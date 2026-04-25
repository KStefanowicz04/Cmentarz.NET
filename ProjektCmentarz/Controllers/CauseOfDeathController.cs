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
    public class CauseOfDeathController : Controller
    {
        private readonly GraveyardContext _context;

        public CauseOfDeathController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: CauseOfDeath
        public async Task<IActionResult> Index()
        {
            return View(await _context.CauseOfDeath.ToListAsync());
        }

        // GET: CauseOfDeath/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var causeOfDeath = await _context.CauseOfDeath
                .FirstOrDefaultAsync(m => m.Id == id);
            if (causeOfDeath == null)
            {
                return NotFound();
            }

            return View(causeOfDeath);
        }

        // GET: CauseOfDeath/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CauseOfDeath/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cause")] CauseOfDeath causeOfDeath)
        {
            if (ModelState.IsValid)
            {
                _context.Add(causeOfDeath);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(causeOfDeath);
        }

        // GET: CauseOfDeath/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var causeOfDeath = await _context.CauseOfDeath.FindAsync(id);
            if (causeOfDeath == null)
            {
                return NotFound();
            }
            return View(causeOfDeath);
        }

        // POST: CauseOfDeath/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cause")] CauseOfDeath causeOfDeath)
        {
            if (id != causeOfDeath.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(causeOfDeath);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CauseOfDeathExists(causeOfDeath.Id))
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
            return View(causeOfDeath);
        }

        // GET: CauseOfDeath/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var causeOfDeath = await _context.CauseOfDeath
                .FirstOrDefaultAsync(m => m.Id == id);
            if (causeOfDeath == null)
            {
                return NotFound();
            }

            return View(causeOfDeath);
        }

        // POST: CauseOfDeath/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var causeOfDeath = await _context.CauseOfDeath.FindAsync(id);
            if (causeOfDeath != null)
            {
                _context.CauseOfDeath.Remove(causeOfDeath);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CauseOfDeathExists(int id)
        {
            return _context.CauseOfDeath.Any(e => e.Id == id);
        }
    }
}
