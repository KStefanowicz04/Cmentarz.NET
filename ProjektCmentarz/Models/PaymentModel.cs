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

        // Wartość opłaty
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
