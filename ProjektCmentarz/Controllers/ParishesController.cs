using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektCmentarz.Data;
using ProjektCmentarz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using X.PagedList.Extensions;
using Microsoft.AspNetCore.Authorization;


namespace ProjektCmentarz.Controllers
{
    public class ParishesController : Controller
    {
        private readonly GraveyardContext _context;

        public ParishesController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: Parishes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parishes.ToListAsync());
        }

        // GET: Parishes/Details/5
        public async Task<IActionResult> Details(int? id, int? page)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parish = await _context.Parishes
                .FirstOrDefaultAsync(m => m.Id == id);

            if (parish == null)
            {
                return NotFound();
            }

            var priestsQuery = _context.Priests.Where(p => p.ParishId == id).OrderBy(p => p.Id);

            int pageNumber = page ?? 1;
            int pageSize = 4;

            ViewBag.Priests = priestsQuery.ToPagedList(pageNumber, pageSize);

            return View(parish);
        }

        // GET: Parishes/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parishes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Parish parish)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parish);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parish);
        }

        // GET: Parishes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parish = await _context.Parishes.FindAsync(id);
            if (parish == null)
            {
                return NotFound();
            }
            return View(parish);
        }

        // POST: Parishes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Parish parish)
        {
            if (id != parish.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParishExists(parish.Id))
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
            return View(parish);
        }

        // GET: Parishes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parish = await _context.Parishes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parish == null)
            {
                return NotFound();
            }

            return View(parish);
        }

        // POST: Parishes/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parish = await _context.Parishes.FindAsync(id);
            if (parish != null)
            {
                _context.Parishes.Remove(parish);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParishExists(int id)
        {
            return _context.Parishes.Any(e => e.Id == id);
        }
    }
}
