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
    public class GravekeepersController : Controller
    {
        private readonly GraveyardContext _context;

        public GravekeepersController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: Gravekeepers
        public async Task<IActionResult> Index()
        {
            var graveyardContext = _context.Gravekeepers.Include(g => g.GravekeeperContactData);
            return View(await graveyardContext.ToListAsync());
        }

        // GET: Gravekeepers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gravekeeper = await _context.Gravekeepers
                .Include(g => g.GravekeeperContactData)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gravekeeper == null)
            {
                return NotFound();
            }

            return View(gravekeeper);
        }

        // GET: Gravekeepers/Create
        public IActionResult Create()
        {
            ViewData["ContactDataId"] = new SelectList(_context.ContactDatas, "Id", "PhoneNumber");
            return View();
        }

        // POST: Gravekeepers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,Surname,ContactDataId")] Gravekeeper gravekeeper)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gravekeeper);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContactDataId"] = new SelectList(_context.ContactDatas, "Id", "PhoneNumber", gravekeeper.ContactDataId);
            return View(gravekeeper);
        }

        // GET: Gravekeepers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gravekeeper = await _context.Gravekeepers.FindAsync(id);
            if (gravekeeper == null)
            {
                return NotFound();
            }
            ViewData["ContactDataId"] = new SelectList(_context.ContactDatas, "Id", "PhoneNumber", gravekeeper.ContactDataId);
            return View(gravekeeper);
        }

        // POST: Gravekeepers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,Surname,ContactDataId")] Gravekeeper gravekeeper)
        {
            if (id != gravekeeper.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gravekeeper);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GravekeeperExists(gravekeeper.Id))
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
            ViewData["ContactDataId"] = new SelectList(_context.ContactDatas, "Id", "PhoneNumber", gravekeeper.ContactDataId);
            return View(gravekeeper);
        }

        // GET: Gravekeepers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gravekeeper = await _context.Gravekeepers
                .Include(g => g.GravekeeperContactData)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gravekeeper == null)
            {
                return NotFound();
            }

            return View(gravekeeper);
        }

        // POST: Gravekeepers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gravekeeper = await _context.Gravekeepers.FindAsync(id);
            if (gravekeeper != null)
            {
                _context.Gravekeepers.Remove(gravekeeper);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GravekeeperExists(int id)
        {
            return _context.Gravekeepers.Any(e => e.Id == id);
        }
    }
}
