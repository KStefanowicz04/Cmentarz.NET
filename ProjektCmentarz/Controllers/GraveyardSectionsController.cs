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
    public class GraveyardSectionsController : Controller
    {
        private readonly GraveyardContext _context;

        public GraveyardSectionsController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: GraveyardSections
        public async Task<IActionResult> Index()
        {
            return View(await _context.GraveyardSection.ToListAsync());
        }

        // GET: GraveyardSections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var graveyardSection = await _context.GraveyardSection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (graveyardSection == null)
            {
                return NotFound();
            }

            return View(graveyardSection);
        }

        // GET: GraveyardSections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GraveyardSections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SectionType")] GraveyardSection graveyardSection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(graveyardSection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(graveyardSection);
        }

        // GET: GraveyardSections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var graveyardSection = await _context.GraveyardSection.FindAsync(id);
            if (graveyardSection == null)
            {
                return NotFound();
            }
            return View(graveyardSection);
        }

        // POST: GraveyardSections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SectionType")] GraveyardSection graveyardSection)
        {
            if (id != graveyardSection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(graveyardSection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GraveyardSectionExists(graveyardSection.Id))
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
            return View(graveyardSection);
        }

        // GET: GraveyardSections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var graveyardSection = await _context.GraveyardSection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (graveyardSection == null)
            {
                return NotFound();
            }

            return View(graveyardSection);
        }

        // POST: GraveyardSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var graveyardSection = await _context.GraveyardSection.FindAsync(id);
            if (graveyardSection != null)
            {
                _context.GraveyardSection.Remove(graveyardSection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GraveyardSectionExists(int id)
        {
            return _context.GraveyardSection.Any(e => e.Id == id);
        }
    }
}
