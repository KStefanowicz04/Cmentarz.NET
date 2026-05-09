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
    public class GravestoneInscryptionsController : Controller
    {
        private readonly GraveyardContext _context;

        public GravestoneInscryptionsController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: GravestoneInscryptions
        public async Task<IActionResult> Index()
        {
            return View(await _context.GravestoneInscryptions.ToListAsync());
        }

        // GET: GravestoneInscryptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gravestoneInscryption = await _context.GravestoneInscryptions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gravestoneInscryption == null)
            {
                return NotFound();
            }

            return View(gravestoneInscryption);
        }

        // GET: GravestoneInscryptions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GravestoneInscryptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Inscryption")] GravestoneInscryption gravestoneInscryption)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gravestoneInscryption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gravestoneInscryption);
        }

        // GET: GravestoneInscryptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gravestoneInscryption = await _context.GravestoneInscryptions.FindAsync(id);
            if (gravestoneInscryption == null)
            {
                return NotFound();
            }
            return View(gravestoneInscryption);
        }

        // POST: GravestoneInscryptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Inscryption")] GravestoneInscryption gravestoneInscryption)
        {
            if (id != gravestoneInscryption.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gravestoneInscryption);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GravestoneInscryptionExists(gravestoneInscryption.Id))
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
            return View(gravestoneInscryption);
        }

        // GET: GravestoneInscryptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gravestoneInscryption = await _context.GravestoneInscryptions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gravestoneInscryption == null)
            {
                return NotFound();
            }

            return View(gravestoneInscryption);
        }

        // POST: GravestoneInscryptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gravestoneInscryption = await _context.GravestoneInscryptions.FindAsync(id);
            if (gravestoneInscryption != null)
            {
                _context.GravestoneInscryptions.Remove(gravestoneInscryption);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GravestoneInscryptionExists(int id)
        {
            return _context.GravestoneInscryptions.Any(e => e.Id == id);
        }
    }
}
