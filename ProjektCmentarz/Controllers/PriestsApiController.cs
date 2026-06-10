using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektCmentarz.Data;
using ProjektCmentarz.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ProjektCmentarz.Controllers
{
    // Stały adres URL dla API
    [Route("api/priests")]
    [ApiController]
    [Authorize]
    public class PriestsApiController : ControllerBase
    {
        private readonly GraveyardContext _context;

        // Konstruktor, który wstrzykuje kontekst bazy danych cmentarza
        public PriestsApiController(GraveyardContext context)
        {
            _context = context;
        }

        // 1. Endpoint: Pobranie wszystkich księży 
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 30)
        {
            // Ustawienia stronicowania
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var totalCount = await _context.Plots.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            var plotList = await _context.Priests
                .OrderBy(p => p.Id)
                .Skip((page-1) * pageSize)
                .Take(pageSize)
                .Select(p => new
                {
                    p.Id,
                    p.ParishId,
                    p.Parish.Name,
                    p.FirstName,
                    p.Surname
                })
                .ToListAsync();

            return Ok(new
            {
                page, pageSize, totalCount, totalPages, data = plotList
            });
        }

        // 2. Endpoint: Pobranie jednego księdza po ID 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var plot = await _context.Priests
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    p.Id,
                    p.ParishId,
                    p.Parish.Name,
                    p.FirstName,
                    p.Surname
                })
                .FirstOrDefaultAsync();

            if (plot == null)
            {
                return NotFound(new { message = $"Nie znaleziono księdza o ID {id}" });
            }

            return Ok(plot);
        }
    }
}