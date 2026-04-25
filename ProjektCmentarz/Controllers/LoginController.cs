using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
    public class LoginController : Controller
    {
        private readonly GraveyardContext _context;

        public LoginController(GraveyardContext context)
        {
            _context = context;
        }

        // GET: Login
        public IActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(LoginModel loginData)
        {
            if (!ModelState.IsValid)
                return View(loginData);

            // Porównujemy Emaile Users w bazie z Emailem podanym w danym powyżej LoginModel 'loginData'
            var user = _context.Users.SingleOrDefault(u => u.Email == loginData.Email);

            // Użytkownik o danym Emailu nie istnieje w bazie
            if (user == null)
            {
                ModelState.AddModelError("", "Nieprawidłowy Email");
                return View(loginData);
            }

            // Jeśli wszystko powyżej się spełniło, dany użytkownik istnieje w bazie.

            // Hashujemy podane w 'loginData' hasło
            var HashedPassword = HashPassword(loginData.Password);

            // Porównujemy zahashowane hasło użytkownika w bazie z powyżej zahashowanym hasłem
            if (!HashedPassword.SequenceEqual(user.Password))
            {
                ModelState.AddModelError("", "Nieprawidłowe hasło");
                return View(loginData);
            }

            // Przygotowanie ciasteczek dla danego użytkownika
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("UserID", user.UserId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
            };

            // Logowanie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);


            return RedirectToAction("Index", "Home");
        }

        // Funkcja hashująca podany string
        public static byte[] HashPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                return sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        // GET: Logout
        // Wylogowuje użytkownika
        [Route("/Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
