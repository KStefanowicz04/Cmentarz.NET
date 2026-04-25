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
    public class PriestsController : Controller
    {
        private readonly GraveyardContext _context;

        public PriestsController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: Priests
        public async Task<IActionResult> Index()
        {
            var graveyardContext = _context.Priests.Include(p => p.Parish).Include(p => p.PriestContactData);
            return View(await graveyardContext.ToListAsync());
        }

        // GET: Priests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priest = await _context.Priests
                .Include(p => p.Parish)
                .Include(p => p.PriestContactData)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (priest == null)
            {
                return NotFound();
            }

            return View(priest);
        }

        // GET: Priests/Create
        public IActionResult Create()
        {
            ViewData["ParishId"] = new SelectList(_context.Parishes, "Id", "Name");
            ViewData["ContactDataId"] = new SelectList(_context.ContactDatas, "Id", "PhoneNumber");
            return View();
        }

        // POST: Priests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,Surname,ContactDataId,ParishId")] Priest priest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(priest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParishId"] = new SelectList(_context.Parishes, "Id", "Name", priest.ParishId);
            ViewData["ContactDataId"] = new SelectList(_context.ContactDatas, "Id", "PhoneNumber", priest.ContactDataId);
            return View(priest);
        }

        // GET: Priests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priest = await _context.Priests.FindAsync(id);
            if (priest == null)
            {
                return NotFound();
            }
            ViewData["ParishId"] = new SelectList(_context.Parishes, "Id", "Name", priest.ParishId);
            ViewData["ContactDataId"] = new SelectList(_context.ContactDatas, "Id", "PhoneNumber", priest.ContactDataId);
            return View(priest);
        }

        // POST: Priests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,Surname,ContactDataId,ParishId")] Priest priest)
        {
            if (id != priest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(priest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriestExists(priest.Id))
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
            ViewData["ParishId"] = new SelectList(_context.Parishes, "Id", "Name", priest.ParishId);
            ViewData["ContactDataId"] = new SelectList(_context.ContactDatas, "Id", "PhoneNumber", priest.ContactDataId);
            return View(priest);
        }

        // GET: Priests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priest = await _context.Priests
                .Include(p => p.Parish)
                .Include(p => p.PriestContactData)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (priest == null)
            {
                return NotFound();
            }

            return View(priest);
        }

        // POST: Priests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var priest = await _context.Priests.FindAsync(id);
            if (priest != null)
            {
                _context.Priests.Remove(priest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriestExists(int id)
        {
            return _context.Priests.Any(e => e.Id == id);
        }
    }
}
