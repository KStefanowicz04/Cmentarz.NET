using Microsoft.EntityFrameworkCore;
using ProjektCmentarz.Models;
using static System.Collections.Specialized.BitVector32;


namespace ProjektCmentarz.Data
{
    // Własny Context dla Cmentarza
    public class GraveyardContext : DbContext
    {
        // Modele w bazie danych
        public virtual DbSet<ContactData> ContactDatas { get; set; }
        public virtual DbSet<Deceased> Deceaseds { get; set; }
        public virtual DbSet<Funeral> Funerals { get; set; }
        public virtual DbSet<Gravekeeper> Gravekeepers { get; set; }
        public virtual DbSet<GraveMaintenance> GraveMaintenances { get; set; }
        public virtual DbSet<Grave> Graves { get; set; }
        public virtual DbSet<Gravestone> Gravestones { get; set; }
        public virtual DbSet<DeathCertificate> DeathCertificates { get; set; }
        public virtual DbSet<Plot> Plots { get; set; }
        public virtual DbSet<PlotOwner> PlotOwners { get; set; }
        public virtual DbSet<Priest> Priests { get; set; }
        public virtual DbSet<GraveyardSection> GraveyardSections { get; set; }

        // Konstruktor
        public GraveyardContext(DbContextOptions options) : base(options) { }


        // Poprawa relacji
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacja 1:1 dla Pogrzeb <-> Nieboszczyk (Pogrzeb jest podrzędny)
            modelBuilder.Entity<Deceased>()
                .HasOne(d => d.Funeral)  // Nieboszczyk ma Pogrzeb
                .WithOne(f => f.Deceased)  // Pogrzeb ma Nieboszczyka
                .HasForeignKey<Funeral>(f => f.DeceasedId);  // Pogrzeb jest podrzędny

            modelBuilder.Entity<Funeral>()
                .HasMany(f => f.FuneralGravekeepers)
                .WithMany(g => g.Funerals)
                .UsingEntity<Dictionary<string, object>>(
                    "FuneralGravekeeper",
                    j => j
                        .HasOne<Gravekeeper>()
                        .WithMany()
                        .HasForeignKey("GravekeeperId")
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j
                        .HasOne<Funeral>()
                        .WithMany()
                        .HasForeignKey("FuneralId")
                        .OnDelete(DeleteBehavior.Restrict)
                );


            //// Relacja wiele:wielu dla Pogrzeby <-> Grabarze
            //modelBuilder.Entity<Funeral>()
            //    .HasMany(f => f.FuneralGravekeepers)  // Pogrzeb ma wiele Grabarzy
            //    .WithMany(g => g.Funerals)  // Grabarz ma wiele pogrzebów
            //    .UsingEntity <Dictionary<string, object>>(
            //        "FuneralGravekeeper",
            //        j => j
            //            .HasOne<Gravekeeper>()
            //            .WithMany()
            //            .HasForeignKey("FuneralGravekeepersId")
            //            .OnDelete(DeleteBehavior.Restrict),
            //        j => j
            //            .HasOne<Funeral>()
            //            .WithMany()
            //            .HasForeignKey("FuneralsId")
            //            .OnDelete(DeleteBehavior.Restrict)
            //    );  // Zmiana ustawień usuwania
        }
    }
}
