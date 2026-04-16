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
    public class GravestonesController : Controller
    {
        private readonly GraveyardContext _context;

        public GravestonesController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: Gravestones
        public async Task<IActionResult> Index()
        {
            var graveyardContext = _context.Gravestones.Include(g => g.Condition).Include(g => g.Grave).Include(g => g.Material);
            return View(await graveyardContext.ToListAsync());
        }

        // GET: Gravestones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gravestone = await _context.Gravestones
                .Include(g => g.Condition)
                .Include(g => g.Grave)
                .Include(g => g.Material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gravestone == null)
            {
                return NotFound();
            }

            return View(gravestone);
        }

        // GET: Gravestones/Create
        public IActionResult Create()
        {
            ViewData["ConditionId"] = new SelectList(_context.Condition, "Id", "ConditionType");
            ViewData["GraveId"] = new SelectList(_context.Graves, "Id", "Id");
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Type");
            return View();
        }

        // POST: Gravestones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InstallationDate,ConditionId,MaterialId,GraveId")] Gravestone gravestone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gravestone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConditionId"] = new SelectList(_context.Condition, "Id", "ConditionType", gravestone.ConditionId);
            ViewData["GraveId"] = new SelectList(_context.Graves, "Id", "Id", gravestone.GraveId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Type", gravestone.MaterialId);
            return View(gravestone);
        }

        // GET: Gravestones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gravestone = await _context.Gravestones.FindAsync(id);
            if (gravestone == null)
            {
                return NotFound();
            }
            ViewData["ConditionId"] = new SelectList(_context.Condition, "Id", "ConditionType", gravestone.ConditionId);
            ViewData["GraveId"] = new SelectList(_context.Graves, "Id", "Id", gravestone.GraveId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Type", gravestone.MaterialId);
            return View(gravestone);
        }

        // POST: Gravestones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InstallationDate,ConditionId,MaterialId,GraveId")] Gravestone gravestone)
        {
            if (id != gravestone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gravestone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GravestoneExists(gravestone.Id))
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
            ViewData["ConditionId"] = new SelectList(_context.Condition, "Id", "ConditionType", gravestone.ConditionId);
            ViewData["GraveId"] = new SelectList(_context.Graves, "Id", "Id", gravestone.GraveId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Type", gravestone.MaterialId);
            return View(gravestone);
        }

        // GET: Gravestones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gravestone = await _context.Gravestones
                .Include(g => g.Condition)
                .Include(g => g.Grave)
                .Include(g => g.Material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gravestone == null)
            {
                return NotFound();
            }

            return View(gravestone);
        }

        // POST: Gravestones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gravestone = await _context.Gravestones.FindAsync(id);
            if (gravestone != null)
            {
                _context.Gravestones.Remove(gravestone);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GravestoneExists(int id)
        {
            return _context.Gravestones.Any(e => e.Id == id);
        }
    }
}
