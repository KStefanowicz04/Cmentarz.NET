using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    // Trumna
    public class Casket
    {
        // ID Trumny; klucz główny
        [Key]
        public int Id { get; set; }

        // Materiał z którego wykonana jest trumna; klucz obcy
        [Display(Name = "Materiał")]
        [ForeignKey("Material")]
        [Required]
        public int MaterialId { get; set; }
        public Material? Material { get; set; }

        // Cena
        [Display(Name = "Cena")]
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        // Klucz obcy do nieboszczyka danej trumny. Nieboszczyk jest nadrzędny!
        [Display(Name = "Nieboszczyk")]
        [ForeignKey("Deceased")]
        public int? DeceasedId { get; set; }
        public Deceased? Deceased { get; set; }
    }
}
