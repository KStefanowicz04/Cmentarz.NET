using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    public class Ownership
    {
        // ID - klucz główny
        [Key]
        public int Id { get; set; }

        // Data nabycia praw do grobu
        [Required]
        public DateTime PurchaseDate { get; set; }

        // Data wygaśnięcia ważności opłaty za grób
        [Required]
        public DateTime ExpirationDate { get; set; }

        // Cena za przedłużenie lub wykupienie własności
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        // Grób, do którego przypisana jest własność
        [Required]
        public int GraveId { get; set; }
        [ForeignKey("GraveId")]
        public virtual Grave Grave { get; set; } = null!;

        // Dane kontaktowe osoby, do której przypisana jest własność
        [Required]
        public int ContactDataId { get; set; }
        [ForeignKey("ContactDataId")]
        public virtual ContactData ContactData { get; set; } = null!;
    }
}
