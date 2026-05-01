using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using ProjektCmentarz.Data;
using ProjektCmentarz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjektCmentarz.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly GraveyardContext _context;

        public UserProfileController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: UserProfile
        [Authorize()]
        public async Task<IActionResult> Index()
        {
            // ID Użytkownika jest pobierane z Claimu UserID przypisanego przy logowaniu
            int userId = int.Parse(User.FindFirst("UserId").Value);

            // Wybieramy użytkownika z bazy danych o tym samym ID co zalogowany użytkownik
            var user = await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // Edytowanie swoich własnych danych przez Użytkownika.
        // GET: /UserProfile/Edit
        public async Task<IActionResult> Edit()
        {
            int userId = int.Parse(User.FindFirst("UserId").Value);

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: /UserProfile/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserProfileEdit userProfileModel)
        {
            if (!ModelState.IsValid)
                return View(userProfileModel);

            var user = await _context.Users.FindAsync(userProfileModel.UserId);
            if (user == null)
                return NotFound();

            // Zmiana podstawowych danych, tu nie można zmienić hasła
            user.FirstName = userProfileModel.FirstName;
            user.Surname = userProfileModel.Surname;
            user.Email = userProfileModel.Email;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        // Zmiana hasła
        // GET: /UserProfile/ChangePassword
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        // POST: /UserProfile/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(UserEditPassword editPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(editPasswordModel);

            int userId = int.Parse(User.FindFirst("UserId").Value);

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound();

            // Porównanie podanego obecnego hasła z bazą po zahashowaniu
            var hashedOldPassword = LoginController.HashPassword(editPasswordModel.OldPassword);
            if (!hashedOldPassword.SequenceEqual(user.Password))
            {
                ModelState.AddModelError("", "Obecne hasło jest nieprawidłowe.");
                return View(editPasswordModel);
            }

            // Jeśli stare hasło zgadza się z bazą danych, zapisujemy nowe hasło
            user.Password = LoginController.HashPassword(editPasswordModel.NewPassword);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
