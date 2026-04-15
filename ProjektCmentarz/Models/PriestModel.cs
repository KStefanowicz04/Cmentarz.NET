using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    // Ksiądz
    public class Priest
    {
        // ID księdza; klucz główny
        [Key]
        public int Id { get; set; }

        // Imię księdza
        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Priest Name is required")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 100 characters")]
        public string FirstName { get; set; }

        // Nazwisko księdza
        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Priest Surname is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Surname should be between 2 and 100 characters")]
        public string Surname { get; set; }

        // Dane kontaktowe
        [Display(Name = "Dane kontaktowe")]
        [ForeignKey("ContactData")]
        public int ContactDataId { get; set; }
        [Display(Name = "Dane kontaktowe")]
        public ContactData? PriestContactData { get; set; }

        // Parafia do której należy ksiądz; klucz obcy
        [Display(Name = "Parafia")]
        [ForeignKey("Parish")]
        [Required(ErrorMessage = "Parish is required")]
        public int ParishId { get; set; }
        [Display(Name = "Parafia")]
        public Parish? Parish { get; set; }

        // Wykonane pogrzeby; lista może być pusta
        public ICollection<Funeral>? Funerals { get; set; }
    }
}
