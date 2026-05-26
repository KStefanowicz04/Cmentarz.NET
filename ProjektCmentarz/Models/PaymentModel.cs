using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    // Opłata
    public class Payment
    {
        // Klucz główny; id opłaty
        [Key]
        public int Id { get; set; }

        // Data wykonania opłaty
        public DateTime? PaymentDate { get; set; }

        // Osoba do której należy dana płatność
        [ForeignKey("Owner")]
        public int? PlotOwnerId { get; set; }
        public PlotOwner? Owner { get; set; }

        // Działka której dotyczy płatność
        public int PlotId { get; set; }
        public Plot Plot { get; set; }

        // Wartość opłaty
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
    }
}
