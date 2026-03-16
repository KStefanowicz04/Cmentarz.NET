using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjektCmentarz.Models
{
    // Grabarz
    public class Gravekeeper
    {
        // ID grabarza; klucz główny
        [Key]
        public int Id { get; set; }

        // Imię grabarza
        [Required(ErrorMessage = "Gravekeeper Name is required")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 100 characters")]
        public string FirstName { get; set; }

        // Nazwisko grabarza
        [Required(ErrorMessage = "Gravekeeper Surname is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Surname should be between 2 and 100 characters")]
        public string Surname { get; set; }
                
        //Identyfikator danych kontaktowych, do których przypisany jest grabarz
        [Required]
        public int ContactDataId { get; set; }
        //Dane kontaktowe, do których przypisany jest grabarz
        [Required]
        [ForeignKey("ContactDataId")]
        public ContactData GravekeeperContactData { get; set; }

        // Wykonane pogrzeby; lista może być pusta
        public virtual ICollection<Funeral>? Funerals { get; set; }
    }
}
