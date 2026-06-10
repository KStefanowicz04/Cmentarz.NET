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
    [Route("api/deceased")]
    [ApiController]
    [Authorize]
    public class DeceasedApiController : ControllerBase
    {
        private readonly GraveyardContext _context;

        // Konstruktor, który wstrzykuje kontekst bazy danych cmentarza
        public DeceasedApiController(GraveyardContext context)
        {
            _context = context;
        }

        // 1. Endpoint: Pobranie wszystkich zmarłych 
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 30)
        {
            // Ustawienia stronicowania
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var totalCount = await _context.Deceaseds.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            var deceasedList = await _context.Deceaseds
                .OrderBy(d => d.Id)
                .Skip((page-1) * pageSize)
                .Take(pageSize)
                .Select(d => new
                {
                    d.Id,
                    d.FirstName,
                    d.Surname,
                    d.BirthDate,
                    d.DeathDate
                })
                .ToListAsync();

            return Ok(new
            {
                page, pageSize, totalCount, totalPages, data = deceasedList
            });
        }

        // 2. Endpoint: Pobranie jednego zmarłego po ID 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var deceased = await _context.Deceaseds
                .Where(d => d.Id == id)
                .Select(d => new
                {
                    d.Id,
                    d.FirstName,
                    d.Surname,
                    d.BirthDate,
                    d.DeathDate
                })
                .FirstOrDefaultAsync();

            if (deceased == null)
            {
                return NotFound(new { message = $"Nie znaleziono zmarłego o ID {id}" });
            }

            return Ok(deceased);
        }
    }
}