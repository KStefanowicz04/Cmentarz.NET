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
    [Route("api/graves")]
    [ApiController]
    [Authorize]
    public class GravesApiController : ControllerBase
    {
        private readonly GraveyardContext _context;

        // Konstruktor, który wstrzykuje kontekst bazy danych cmentarza
        public GravesApiController(GraveyardContext context)
        {
            _context = context;
        }

        // 1. Endpoint: Pobranie wszystkich grobów 
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 30)
        {
            // Ustawienia stronicowania
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var totalCount = await _context.Plots.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            var plotList = await _context.Graves
                .OrderBy(g => g.Id)
                .Skip((page-1) * pageSize)
                .Take(pageSize)
                .Select(g => new
                {
                    g.Id,
                    g.PlotId,
                    g.DeceasedId,
                    g.GraveDeceased.FirstName,
                    g.GraveDeceased.Surname,
                    g.GravestoneId
                })
                .ToListAsync();

            return Ok(new
            {
                page, pageSize, totalCount, totalPages, data = plotList
            });
        }

        // 2. Endpoint: Pobranie jednego grobu po ID 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var plot = await _context.Graves
                .Where(g => g.Id == id)
                .Select(g => new
                {
                    g.Id,
                    g.PlotId,
                    g.DeceasedId,
                    g.GraveDeceased.FirstName,
                    g.GraveDeceased.Surname,
                    g.GravestoneId,
                })
                .FirstOrDefaultAsync();

            if (plot == null)
            {
                return NotFound(new { message = $"Nie znaleziono grobu o ID {id}" });
            }

            return Ok(plot);
        }
    }
}