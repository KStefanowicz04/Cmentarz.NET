using System.ComponentModel.DataAnnotations;

namespace ProjektCmentarz.Models
{
    // Groby, urny, trumny mogą być zrobione z jednego z wielu materiałów
    public class Material
    {
        // Id materiału; klucz główny 
        [Key]
        public int Id { get; set; }

        // Nazwa materiału
        [Display(Name = "Materiał")]
        [Required(ErrorMessage = "Material type is required")]
        [StringLength(100)]
        public string Type { get; set; }    
    }
}
