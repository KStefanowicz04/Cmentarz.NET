using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    public class Urn
    {
        [Key]
        public int Id { get; set; }

        // Model lub nazwa urny 
        [Required(ErrorMessage = "Urn name is required")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        // Materiał z którego wykonana jest urna; klucz obcy
        [ForeignKey("Material")]
        [Required(ErrorMessage = "Urn material is required")]
        public int MaterialId { get; set; }
        public Material Material { get; set; }

        // Pojemność w litrach 
        [Range(0.1, 10.0)]
        public double Capacity { get; set; }

        // Cena
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        // Jeden pogrzeb to jedna urna, dodajemy klucz obcy do Funeral
        [ForeignKey("Funeral")]
        public int? FuneralId { get; set; }
        public virtual Funeral? Funeral { get; set; }
    }
}
