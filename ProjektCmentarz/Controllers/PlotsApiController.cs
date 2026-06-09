using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektCmentarz.Data;
using ProjektCmentarz.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektCmentarz.Controllers
{
    // Stały adres URL dla API
    [Route("api/plots")]
    [ApiController]
    public class PlotsApiController : ControllerBase
    {
        private readonly GraveyardContext _context;

        // Konstruktor, który wstrzykuje kontekst bazy danych cmentarza
        public PlotsApiController(GraveyardContext context)
        {
            _context = context;
        }

        // 1. Endpoint: Pobranie wszystkich działek 
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 30)
        {
            // Ustawienia stronicowania
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var totalCount = await _context.Plots.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            var plotList = await _context.Plots
                .OrderBy(p => p.Id)
                .Skip((page-1) * pageSize)
                .Take(pageSize)
                .Select(p => new
                {
                    p.Id,
                    p.GraveyardSection.SectionType,
                    p.PlotOwnerId,
                    p.PlotValue,
                })
                .ToListAsync();

            return Ok(new
            {
                page, pageSize, totalCount, totalPages, data = plotList
            });
        }

        // 2. Endpoint: Pobranie jednej działki po ID 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var plot = await _context.Plots
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    p.Id,
                    p.GraveyardSection.SectionType,
                    p.PlotOwnerId,
                    p.PlotValue
                })
                .FirstOrDefaultAsync();

            if (plot == null)
            {
                return NotFound(new { message = $"Nie znaleziono działki o ID {id}" });
            }

            return Ok(plot);
        }
    }
}