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
    public class DeceasedController : Controller
    {
        private readonly GraveyardContext _context;

        public DeceasedController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: Deceased
        public async Task<IActionResult> Index(string searchFirstName, string searchSurname, DateTime? searchDate)
        {
            
            var deceaseds = from d in _context.Deceaseds
                            select d;
            
            if (!string.IsNullOrEmpty(searchFirstName))
            {
                deceaseds = deceaseds.Where(s => s.FirstName.Contains(searchFirstName));
            }

            if (!string.IsNullOrEmpty(searchSurname))
            {
                deceaseds = deceaseds.Where(s => s.Surname.Contains(searchSurname));
            }
         
            if (searchDate.HasValue)
            {
                deceaseds = deceaseds.Where(s => s.DeathDate.Date == searchDate.Value.Date);
            }
         
            ViewData["CurrentFirstName"] = searchFirstName;
            ViewData["CurrentSurname"] = searchSurname;
            ViewData["CurrentDate"] = searchDate?.ToString("yyyy-MM-dd");
          
            return View(await deceaseds.OrderByDescending(d => d.DeathDate).ToListAsync());
        }

        // GET: Deceased/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deceased = await _context.Deceaseds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deceased == null)
            {
                return NotFound();
            }

            return View(deceased);
        }

        // GET: Deceased/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Deceased/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,Surname,BirthDate,DeathDate,CasketId,FuneralId")] Deceased deceased)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deceased);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deceased);
        }

        // GET: Deceased/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deceased = await _context.Deceaseds.FindAsync(id);
            if (deceased == null)
            {
                return NotFound();
            }
            return View(deceased);
        }

        // POST: Deceased/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,Surname,BirthDate,DeathDate,CasketId,FuneralId")] Deceased deceased)
        {
            if (id != deceased.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deceased);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeceasedExists(deceased.Id))
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
            return View(deceased);
        }

        // GET: Deceased/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deceased = await _context.Deceaseds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deceased == null)
            {
                return NotFound();
            }

            return View(deceased);
        }

        // POST: Deceased/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deceased = await _context.Deceaseds.FindAsync(id);
            if (deceased != null)
            {
                _context.Deceaseds.Remove(deceased);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeceasedExists(int id)
        {
            return _context.Deceaseds.Any(e => e.Id == id);
        }
    }
}
