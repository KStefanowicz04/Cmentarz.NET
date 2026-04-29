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
    public class UsersController : Controller
    {
        private readonly GraveyardContext _context;

        public UsersController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.Include(u => u.Roles).ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FirstName,Surname,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
                return NotFound();

            var model = new UserEdit
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                Surname = user.Surname,
                Email = user.Email,
                RoleIds = user.Roles.Select(r => r.Id).ToList()
            };

            ViewBag.Roles = new MultiSelectList(
                await _context.Roles.ToListAsync(),
                "Id",
                "RoleName",
                model.RoleIds
            );

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserEdit model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new MultiSelectList(
                    await _context.Roles.ToListAsync(),
                    "Id",
                    "RoleName",
                    model.RoleIds
                );
                return View(model);
            }

            var user = await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
                return NotFound();

            // Update basic fields
            user.FirstName = model.FirstName;
            user.Surname = model.Surname;
            user.Email = model.Email;

            // Update roles
            user.Roles.Clear();

            if (model.RoleIds != null)
            {
                var roles = await _context.Roles
                    .Where(r => model.RoleIds.Contains(r.Id))
                    .ToListAsync();

                foreach (var role in roles)
                    user.Roles.Add(role);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
