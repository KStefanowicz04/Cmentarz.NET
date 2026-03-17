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
        [ForeignKey("Material")]
        [Required(ErrorMessage = "Casket material is required")]
        public int MaterialId { get; set; }
        public Material Material { get; set; }

        // Cena
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        // Klucz obcy do nieboszczyka danej trumny. Nieboszczyk jest nadrzędny!
        [ForeignKey("Deceased")]
        [Required(ErrorMessage = "A casket belongs to a Deceased")]
        public int DeceasedId { get; set; }
        public Deceased Deceased { get; set; }
    }
}
