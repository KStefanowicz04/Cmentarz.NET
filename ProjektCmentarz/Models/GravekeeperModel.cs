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
        [Display(Name = "Imię grabarza")]
        [Required(ErrorMessage = "Imię grabarza jest wymagana")]
        [StringLength(64, MinimumLength = 2, ErrorMessage = "Imię ma od 2 and 64 znaków")]
        public string FirstName { get; set; }

        // Nazwisko grabarza
        [Display(Name = "Nazwisko grabarza")]
        [Required(ErrorMessage = "Nazwisko grabarza jest wymagana")]
        [StringLength(128, MinimumLength = 2, ErrorMessage = "Nazwisko ma od 2 do 128 znaków")]
        public string Surname { get; set; }
                
        //Identyfikator danych kontaktowych, do których przypisany jest grabarz
        [Required]
        public int ContactDataId { get; set; }
        // Dane kontaktowe, do których przypisany jest grabarz
        [Display(Name = "Dane kontaktowe grabarza")]
        [Required]
        [ForeignKey("ContactDataId")]
        public ContactData? GravekeeperContactData { get; set; }

        // Wykonane pogrzeby; lista może być pusta
        public virtual ICollection<Funeral>? Funerals { get; set; }
    }
}
