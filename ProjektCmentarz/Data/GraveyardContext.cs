using Microsoft.EntityFrameworkCore;
using ProjektCmentarz.Models;


namespace ProjektCmentarz.Data
{
    // Własny Context dla Cmentarza
    public class GraveyardContext : DbContext
    {
        // Nieboszczyki w bazie danych
        public virtual DbSet<Deceased> Deceaseds { get; set; }

        // Konstruktor
        public GraveyardContext(DbContextOptions options) : base(options) { }
    }
}
