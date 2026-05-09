using Microsoft.EntityFrameworkCore;
using ProjektCmentarz.Models;
using static System.Collections.Specialized.BitVector32;


namespace ProjektCmentarz.Data
{
    // Własny Context dla Cmentarza
    public class GraveyardContext : DbContext
    {
        // Modele w bazie danych
        public virtual DbSet<CauseOfDeath> CausesOfDeath { get; set; }
        public virtual DbSet<ContactData> ContactDatas { get; set; }
        public virtual DbSet<Cremation> Cremations { get; set; }
        public virtual DbSet<DeathCertificate> DeathCertificates { get; set; }
        public virtual DbSet<Deceased> Deceaseds { get; set; }
        public virtual DbSet<FuneralHome> FuneralHomes { get; set; }
        public virtual DbSet<Funeral> Funerals { get; set; }
        public virtual DbSet<Gravekeeper> Gravekeepers { get; set; }
        public virtual DbSet<GraveMaintenance> GraveMaintenances { get; set; }
        public virtual DbSet<Grave> Graves { get; set; }
        public virtual DbSet<GravestoneInscryption> GravestoneInscryptions { get; set; }
        public virtual DbSet<Gravestone> Gravestones { get; set; }
        public virtual DbSet<GraveyardSection> Sections { get; set; }
        public virtual DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<Ownership> Ownerships { get; set; }
        public virtual DbSet<Parish> Parishes { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Plot> Plots { get; set; }
        public virtual DbSet<PlotOwner> PlotOwners { get; set; }
        public virtual DbSet<Priest> Priests { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Transfer> Transfers { get; set; }
        public virtual DbSet<User> Users { get; set; }  // Użytkownicy!

        // Konstruktor
        public GraveyardContext(DbContextOptions options) : base(options) { }


        // Poprawa relacji
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacja 1:1 dla Pogrzeb <-> Nieboszczyk (Pogrzeb jest podrzędny)
            modelBuilder.Entity<Deceased>()
                .HasOne(d => d.Funeral)  // Nieboszczyk ma Pogrzeb
                .WithOne(f => f.Deceased)  // Pogrzeb ma Nieboszczyka
                .HasForeignKey<Funeral>(f => f.DeceasedId)  // Pogrzeb jest podrzędny
                .IsRequired(false)  // Nieboszczyk może istnieć w bazie bez pogrzebu
                .OnDelete(DeleteBehavior.Cascade);  // Pogrzeb jest usuwany przy usunięciu nieboszczyka

            // Relacja 1:1 dla Trumna <-> Nieboszczyk (Trumna jest podrzędna)
            modelBuilder.Entity<Deceased>()
                .HasOne(d => d.Casket)  // Nieboszczyk ma Trumnę
                .WithOne(f => f.Deceased)  // Trumna ma Nieboszczyka
                .HasForeignKey<Casket>(f => f.DeceasedId)  // Trumna jest podrzędną
                .IsRequired(false)  // Nieboszczyk może istnieć w bazie bez trumny
                .OnDelete(DeleteBehavior.Cascade);  // Trumna jest usuwana przy usunięciu nieboszczyka

            // Relacja wiele:wielu dla Gravekeeper <-> Funeral
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

            // Usunięcie ON DELETE CASCADE z Transfer, ponieważ
            // Model Grave pojawia się tam 2 razy
            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.TransferFromGrave)
                .WithMany()
                .HasForeignKey(t => t.FromGraveId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.TransferToGrave)
                .WithMany()
                .HasForeignKey(t => t.ToGraveId)
                .OnDelete(DeleteBehavior.Restrict);

            // Przy usuwaniu Funeral, nie usuwamy Plot
            modelBuilder.Entity<Funeral>()
                .HasOne(f => f.FuneralPlot)
                .WithMany()
                .HasForeignKey(f => f.PlotId)
                .OnDelete(DeleteBehavior.Restrict);

            // Przy usuwaniu Gravekeeper, nie usuwamy ContactData
            modelBuilder.Entity<Gravekeeper>()
                .HasOne(g => g.GravekeeperContactData)
                .WithMany()
                .HasForeignKey(g => g.ContactDataId)
                .OnDelete(DeleteBehavior.Restrict);

            // Przy usuwaniu właności, nie usuwamy grobu
            modelBuilder.Entity<Ownership>()
                .HasOne(o => o.Grave)
                .WithMany()
                .HasForeignKey(o => o.GraveId)
                .OnDelete(DeleteBehavior.Restrict);

            // Przy usuwaniu właności nie usuwamy danych kontaktowych
            modelBuilder.Entity<Ownership>()
                .HasOne(o => o.ContactData)
                .WithMany()
                .HasForeignKey(o => o.ContactDataId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<ProjektCmentarz.Models.Casket> Casket { get; set; } = default!;
        public DbSet<ProjektCmentarz.Models.BurialDepth> BurialDepth { get; set; } = default!;
        public DbSet<ProjektCmentarz.Models.Condition> Condition { get; set; } = default!;
        public DbSet<ProjektCmentarz.Models.GraveyardSection> GraveyardSection { get; set; } = default!;
    }
}
