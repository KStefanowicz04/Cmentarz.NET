using System.ComponentModel.DataAnnotations;

namespace ProjektCmentarz.Models
{
    // Przypadek czyszczenia danego grobu przez grabarza
    public class GraveMaintenance
    {
        // ID; klucz główny
        [Key]
        public int Id { get; set; }

        // Grabarz, który wyczyścił grób
        [Required(ErrorMessage = "Gravekeeper is required")]
        public Gravekeeper MaintenanceGravekeeper { get; set; }
    }
}
