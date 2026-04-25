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
    public class ContactDataController : Controller
    {
        private readonly GraveyardContext _context;

        public ContactDataController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: ContactData
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContactDatas.ToListAsync());
        }

        // GET: ContactData/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactData = await _context.ContactDatas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactData == null)
            {
                return NotFound();
            }

            return View(contactData);
        }

        // GET: ContactData/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PhoneNumber,EMail,CityName,StreetName,ZipCode")] ContactData contactData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactData);
        }

        // GET: ContactData/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactData = await _context.ContactDatas.FindAsync(id);
            if (contactData == null)
            {
                return NotFound();
            }
            return View(contactData);
        }

        // POST: ContactData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PhoneNumber,EMail,CityName,StreetName,ZipCode")] ContactData contactData)
        {
            if (id != contactData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactDataExists(contactData.Id))
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
            return View(contactData);
        }

        // GET: ContactData/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactData = await _context.ContactDatas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactData == null)
            {
                return NotFound();
            }

            return View(contactData);
        }

        // POST: ContactData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactData = await _context.ContactDatas.FindAsync(id);
            if (contactData != null)
            {
                _context.ContactDatas.Remove(contactData);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactDataExists(int id)
        {
            return _context.ContactDatas.Any(e => e.Id == id);
        }
    }
}
