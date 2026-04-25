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
    public class DeathCertificatesController : Controller
    {
        private readonly GraveyardContext _context;

        public DeathCertificatesController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: DeathCertificates
        public async Task<IActionResult> Index()
        {
            var graveyardContext = _context.DeathCertificates.Include(d => d.CauseOfDeath).Include(d => d.Deceased);
            return View(await graveyardContext.ToListAsync());
        }

        // GET: DeathCertificates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deathCertificate = await _context.DeathCertificates
                .Include(d => d.CauseOfDeath)
                .Include(d => d.Deceased)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deathCertificate == null)
            {
                return NotFound();
            }

            return View(deathCertificate);
        }

        // GET: DeathCertificates/Create
        public IActionResult Create()
        {
            ViewData["CauseOfDeathId"] = new SelectList(_context.CausesOfDeath, "Id", "Cause");
            ViewData["DeceasedId"] = new SelectList(_context.Deceaseds, "Id", "FirstName");
            return View();
        }

        // POST: DeathCertificates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IssueDate,CauseOfDeathId,Issuer,DeceasedId")] DeathCertificate deathCertificate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deathCertificate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CauseOfDeathId"] = new SelectList(_context.CausesOfDeath, "Id", "Cause", deathCertificate.CauseOfDeathId);
            ViewData["DeceasedId"] = new SelectList(_context.Deceaseds, "Id", "FirstName", deathCertificate.DeceasedId);
            return View(deathCertificate);
        }

        // GET: DeathCertificates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deathCertificate = await _context.DeathCertificates.FindAsync(id);
            if (deathCertificate == null)
            {
                return NotFound();
            }
            ViewData["CauseOfDeathId"] = new SelectList(_context.CausesOfDeath, "Id", "Cause", deathCertificate.CauseOfDeathId);
            ViewData["DeceasedId"] = new SelectList(_context.Deceaseds, "Id", "FirstName", deathCertificate.DeceasedId);
            return View(deathCertificate);
        }

        // POST: DeathCertificates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IssueDate,CauseOfDeathId,Issuer,DeceasedId")] DeathCertificate deathCertificate)
        {
            if (id != deathCertificate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deathCertificate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeathCertificateExists(deathCertificate.Id))
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
            ViewData["CauseOfDeathId"] = new SelectList(_context.CausesOfDeath, "Id", "Cause", deathCertificate.CauseOfDeathId);
            ViewData["DeceasedId"] = new SelectList(_context.Deceaseds, "Id", "FirstName", deathCertificate.DeceasedId);
            return View(deathCertificate);
        }

        // GET: DeathCertificates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deathCertificate = await _context.DeathCertificates
                .Include(d => d.CauseOfDeath)
                .Include(d => d.Deceased)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deathCertificate == null)
            {
                return NotFound();
            }

            return View(deathCertificate);
        }

        // POST: DeathCertificates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deathCertificate = await _context.DeathCertificates.FindAsync(id);
            if (deathCertificate != null)
            {
                _context.DeathCertificates.Remove(deathCertificate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeathCertificateExists(int id)
        {
            return _context.DeathCertificates.Any(e => e.Id == id);
        }
    }
}
