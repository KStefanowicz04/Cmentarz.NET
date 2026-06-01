using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace ProjektCmentarz.Models
{
    public class DeathCertificate
    {
        // Id aktu zgonu; klucz główny
        [Key]
        public int Id { get; set; }

        // Data wystawienia aktu zgonu
        [Display(Name = "Data wystawienia aktu zgonu")]
        [Required(ErrorMessage = "Issue date is required")]
        public DateTime IssueDate { get; set; }

        // Przyczyna śmierci
        [ForeignKey("CauseOfDeath")]
        [Required(ErrorMessage = "Cause of Death is required")]
        public int CauseOfDeathId { get; set; }
        public CauseOfDeath? CauseOfDeath { get; set; }

        // Urząd który wystawił dokument - może należy dodać do tego encję słownikową?
        [ForeignKey("Issuer")]
        [Required(ErrorMessage = "DeathCertificate requires an Issuer")]
        public int IssuerId { get; set; }
        // Dom pogrzebowy, który wydał certyfikat
        public FuneralHome? Issuer { get; set; }


        // Klucz obcy do zmarłego, którego dotyczy akt zgonu
        [ForeignKey("Deceased")]
        [Required(ErrorMessage = "DeathCertificate must belong to a Deceased")]
        public int DeceasedId { get; set; }
        // Zmarły, którego dotyczy akt zgonu
        public Deceased? Deceased { get; set; }
    }
}
