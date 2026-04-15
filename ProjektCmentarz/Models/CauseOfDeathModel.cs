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
        [Display(Name = "Przyczyna śmierci")]
        [Required(ErrorMessage = "Przyczyna śmierci jest wymagana.")]
        [StringLength(100)]
        public string Cause { get; set; }
    }
}
