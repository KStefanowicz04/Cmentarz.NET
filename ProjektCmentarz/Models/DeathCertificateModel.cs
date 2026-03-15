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
        [Required(ErrorMessage ="Issue date is required")]
        public DateTime IssueDate { get; set; }

        // Urząd który wystawił dokument - może należy dodać do tego encję słownikową?
        [Required(ErrorMessage = "Issuing authority is required")]
        [StringLength(100)]
        public string Issuer { get; set; }

        // Klucz obcy do zmarłego, którego dotyczy akt zgonu
        [ForeignKey("Deceased")]
        [Required(ErrorMessage = "DeathCertificate must belong to a Deceased")]
        public int DeceasedId { get; set; }
        // Zmarły, którego dotyczy akt zgonu
        public Deceased Deceased { get; set; }
    }
}
