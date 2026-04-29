using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektCmentarz.Data;
using ProjektCmentarz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjektCmentarz.Controllers
{
    public class RegisterController : Controller
    {
        private readonly GraveyardContext _context;

        public RegisterController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: Register
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
                return View(registerModel);

            // Utworzenie nowego użytkownika User o danych podanych w registerModel
            var user = new User
            {
                FirstName = registerModel.Name,
                Surname = registerModel.Surname,
                Email = registerModel.Email,
                Password = HashPassword(registerModel.Password),  // Hasło zostanie zapisane w bazie jako hash
                // Domyślnie każdy użytkownik otrzymuje rolę "Użytkownik"
                Roles = new List<Role> { await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == "Użytkownik") }
            };

            // Dodanie użytkownika do bazy
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Login");
        }

        // Funkcja hashująca podany string
        public static byte[] HashPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                return sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        // GET: Rejestracja/Sukces
        public async Task<IActionResult> Sukces()
        {
            return View();
        }
    }
}
