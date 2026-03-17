using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    // Głębokość pochówku
    public class BurialDepth
    {
        // ID głębokości; klucz główny
        [Key]
        public int Id { get; set; }

        // Głębokość
        [Required(ErrorMessage = "Depth is required")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Burial depth name should be between 2 and 100 characters")]
        public string Depth { get; set; }
    }
}
