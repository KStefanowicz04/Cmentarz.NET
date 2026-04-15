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
    public class BurialDepthsController : Controller
    {
        private readonly GraveyardContext _context;

        public BurialDepthsController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: BurialDepths
        public async Task<IActionResult> Index()
        {
            return View(await _context.BurialDepth.ToListAsync());
        }

        // GET: BurialDepths/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var burialDepth = await _context.BurialDepth
                .FirstOrDefaultAsync(m => m.Id == id);
            if (burialDepth == null)
            {
                return NotFound();
            }

            return View(burialDepth);
        }

        // GET: BurialDepths/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BurialDepths/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Depth")] BurialDepth burialDepth)
        {
            if (ModelState.IsValid)
            {
                _context.Add(burialDepth);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(burialDepth);
        }

        // GET: BurialDepths/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var burialDepth = await _context.BurialDepth.FindAsync(id);
            if (burialDepth == null)
            {
                return NotFound();
            }
            return View(burialDepth);
        }

        // POST: BurialDepths/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Depth")] BurialDepth burialDepth)
        {
            if (id != burialDepth.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(burialDepth);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BurialDepthExists(burialDepth.Id))
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
            return View(burialDepth);
        }

        // GET: BurialDepths/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var burialDepth = await _context.BurialDepth
                .FirstOrDefaultAsync(m => m.Id == id);
            if (burialDepth == null)
            {
                return NotFound();
            }

            return View(burialDepth);
        }

        // POST: BurialDepths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var burialDepth = await _context.BurialDepth.FindAsync(id);
            if (burialDepth != null)
            {
                _context.BurialDepth.Remove(burialDepth);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BurialDepthExists(int id)
        {
            return _context.BurialDepth.Any(e => e.Id == id);
        }
    }
}
