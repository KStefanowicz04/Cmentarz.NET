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
        private readonly String _ocrApiKey;

        // Dodatkowo podajemy config, bo tam jest klucz API dla OCR-Space
        public RegisterController(GraveyardContext context, IConfiguration config)
        {
            _context = context;
            _ocrApiKey = config["OCR:apikey"];
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

            // Wykorzystanie API OCR Space do odczytania słów z podanego przez użytkownika zdjęcia
            var client = new HttpClient();
            var url = "https://api.ocr.space/parse/image";  // Interfejs od OCR Space do odczytywania obrazów
            var form = new MultipartFormDataContent();
            form.Add(new StringContent(_ocrApiKey), "apikey");  // Klucz API zarejestrowany na moim EMailu

            // Odczytanie informacji z podanego przez użytkownika zdjęcia
            using (var stream = registerModel.PolImage.OpenReadStream())
            {
                form.Add(new StreamContent(stream), "file", registerModel.PolImage.FileName);
                form.Add(new StringContent("pol"), "language");  // Język polski

                // Wysłanie formularza i odpowiedź
                var response = await client.PostAsync(url, form);
                var json = await response.Content.ReadAsStringAsync();

                // Jeśli podane zdjęcie zawiera słowo "POLSAT", uznajemy że to zdjęcie przedstawia Paszport Polsatu.
                // Jeśli nie, użytkownik nie może się zarejestrować.
                if (!json.Contains("POLSAT", StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError("", "Zdjęcie musi przedstawiać Paszportu POLSATu!");
                    return View(registerModel);
                }
            }

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
