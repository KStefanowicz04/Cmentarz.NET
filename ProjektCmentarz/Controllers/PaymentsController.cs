using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektCmentarz.Data;
using ProjektCmentarz.Models;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjektCmentarz.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly GraveyardContext _context;

        public PaymentsController(GraveyardContext context)
        {
            _context = context;
        }

        // Opłata działki poprzez API Stripe
        public IActionResult Pay(int id)
        {
            // Wybieramy przed chwilą utworzony Payment z bazy danych
            var payment = _context.Payments
                .Include(p => p.Plot)
                .Include(p => p.Owner)
                .FirstOrDefault(p => p.Id == id);
            if (payment == null)
                return NotFound();

            // Utworzenie sesji Stripe
            // Domena naszej aplikacji
            var domain = $"{Request.Scheme}://{Request.Host}";
            // Opcje sesji
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmountDecimal = payment.Price * 100,
                            Currency = "pln",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = $"Opłata za działkę #{payment.PlotId}"
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = domain + $"/Payments/Success?id={payment.Id}",
                CancelUrl = domain + $"/Payments/Cancel?id={payment.Id}"
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);
        }

        // Opłata przebiegła pomyślnie
        public async Task<IActionResult> Success(int id)
        {
            var payment = await _context.Payments
                .Include(p => p.Plot)
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (payment == null)
                return NotFound();

            // Ustalenie daty wykonania opłaty
            payment.PaymentDate = DateTime.Now;
            // Przypisanie Działki do Właściciela
            payment.Plot.PlotOwnerId = payment.PlotOwnerId;
            await _context.SaveChangesAsync();

            return View(payment);
        }

        // Opłata NIE przebiegła pomyślnie
        public async Task<IActionResult> Cancel(int id)
        {
            // Usunięcie wcześniej utworzonego, niekompletnego rekordu płatności
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.Id == id);
            if (payment != null && payment.PaymentDate == null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "RentPlot");
        }
    }
}
