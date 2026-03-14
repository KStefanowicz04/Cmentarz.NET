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
        [Required(ErrorMessage = "Priest Name is required")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 100 characters")]
        public string FirstName { get; set; }

        // Nazwisko księdza
        [Required(ErrorMessage = "Priest Surname is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Surname should be between 2 and 100 characters")]
        public string Surname { get; set; }

        // Dane kontaktowe
        [ForeignKey("ContactData")]
        public int ContactDataId { get; set; }
        public ContactData PriestContactData { get; set; }

        // Wykonane pogrzeby
        public ICollection<Funeral> Funerals { get; set; }
    }
}
