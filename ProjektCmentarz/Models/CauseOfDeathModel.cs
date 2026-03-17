using System.ComponentModel.DataAnnotations;

namespace ProjektCmentarz.Models
{
    // Przyczyna śmierci
    public class CauseOfDeath
    {
        // ID przyczyny; klucz główny 
        [Key]
        public int Id { get; set; }

        // Przyczyna śmierci
        [Required(ErrorMessage = "Cause of Death is required")]
        [StringLength(100)]
        public string Cause { get; set; }
    }
}
