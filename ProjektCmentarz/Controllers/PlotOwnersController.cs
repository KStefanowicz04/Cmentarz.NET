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
    public class PlotOwnersController : Controller
    {
        private readonly GraveyardContext _context;

        public PlotOwnersController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: PlotOwners
        public async Task<IActionResult> Index()
        {
            var graveyardContext = _context.PlotOwners.Include(p => p.OwnerContactData);
            return View(await graveyardContext.ToListAsync());
        }

        // GET: PlotOwners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plotOwner = await _context.PlotOwners
                .Include(p => p.OwnerContactData)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plotOwner == null)
            {
                return NotFound();
            }

            return View(plotOwner);
        }

        // GET: PlotOwners/Create
        public IActionResult Create()
        {
            ViewData["ContactDataId"] = new SelectList(_context.ContactDatas, "Id", "PhoneNumber");
            return View();
        }

        // POST: PlotOwners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,Surname,ContactDataId")] PlotOwner plotOwner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plotOwner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContactDataId"] = new SelectList(_context.ContactDatas, "Id", "PhoneNumber", plotOwner.ContactDataId);
            return View(plotOwner);
        }

        // GET: PlotOwners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plotOwner = await _context.PlotOwners.FindAsync(id);
            if (plotOwner == null)
            {
                return NotFound();
            }
            ViewData["ContactDataId"] = new SelectList(_context.ContactDatas, "Id", "PhoneNumber", plotOwner.ContactDataId);
            return View(plotOwner);
        }

        // POST: PlotOwners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,Surname,ContactDataId")] PlotOwner plotOwner)
        {
            if (id != plotOwner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plotOwner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlotOwnerExists(plotOwner.Id))
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
            ViewData["ContactDataId"] = new SelectList(_context.ContactDatas, "Id", "PhoneNumber", plotOwner.ContactDataId);
            return View(plotOwner);
        }

        // GET: PlotOwners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plotOwner = await _context.PlotOwners
                .Include(p => p.OwnerContactData)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plotOwner == null)
            {
                return NotFound();
            }

            return View(plotOwner);
        }

        // POST: PlotOwners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plotOwner = await _context.PlotOwners.FindAsync(id);
            if (plotOwner != null)
            {
                _context.PlotOwners.Remove(plotOwner);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlotOwnerExists(int id)
        {
            return _context.PlotOwners.Any(e => e.Id == id);
        }
    }
}
