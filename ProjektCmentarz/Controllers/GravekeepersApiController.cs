using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektCmentarz.Data;
using ProjektCmentarz.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektCmentarz.Controllers
{
    // Stały adres URL dla API
    [Route("api/gravekeepers")]
    [ApiController]
    public class GravekeepersApiController : ControllerBase
    {
        private readonly GraveyardContext _context;

        // Konstruktor, który wstrzykuje kontekst bazy danych cmentarza
        public GravekeepersApiController(GraveyardContext context)
        {
            _context = context;
        }

        // 1. Endpoint: Pobranie wszystkich grabarzy 
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 30)
        {
            // Ustawienia stronicowania
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var totalCount = await _context.Plots.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            var plotList = await _context.Gravekeepers
                .OrderBy(g => g.Id)
                .Skip((page-1) * pageSize)
                .Take(pageSize)
                .Select(g => new
                {
                    g.Id,
                    g.FirstName,
                    g.Surname
                })
                .ToListAsync();

            return Ok(new
            {
                page, pageSize, totalCount, totalPages, data = plotList
            });
        }

        // 2. Endpoint: Pobranie jednego grabarza po ID 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var plot = await _context.Gravekeepers
                .Where(g => g.Id == id)
                .Select(g => new
                {
                    g.Id,
                    g.FirstName,
                    g.Surname
                })
                .FirstOrDefaultAsync();

            if (plot == null)
            {
                return NotFound(new { message = $"Nie znaleziono grabarza o ID {id}" });
            }

            return Ok(plot);
        }
    }
}