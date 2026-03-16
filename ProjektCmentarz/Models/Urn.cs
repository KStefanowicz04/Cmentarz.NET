using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    public class Urn
    {
        [Key]
        public int Id { get; set; }

        // Model lub nazwa urny 
        [Required(ErrorMessage = "Urn model name is required")]
        [StringLength(100)]
        public string ModelName { get; set; } = string.Empty;

        // Materiał wykonania (np. Kamień, Drewno, Metal, Kompozyt)
        [Required]
        [StringLength(50)]
        public string Material { get; set; } = string.Empty;

        // Pojemność w litrach 
        [Range(0.1, 10.0)]
        public double Capacity { get; set; }

        // Cena
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        // Jeden pogrzeb to jedna urna, dodajemy klucz obcy do Funeral
        public int? FuneralId { get; set; }

        [ForeignKey("FuneralId")]
        public virtual Funeral? Funeral { get; set; }
    }
}
