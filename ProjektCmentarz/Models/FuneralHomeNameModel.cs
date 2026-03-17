using System.ComponentModel.DataAnnotations;

namespace ProjektCmentarz.Models
{
    // Nazwy domów pogrzebowych
    public class FuneralHomeName
    {
        // ID nazwy domu; klucz główny 
        [Key]
        public int Id { get; set; }

        // Nazwa domu pogrzebowego 
        [Required(ErrorMessage = "Funeral Home anme is required")]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
