using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    // Właściciel działki
    public class PlotOwner
    {
        // ID właściciela; klucz główny
        [Key]
        public int Id { get; set; }

        // Imię właściciela
        [Required(ErrorMessage = "Plot Owner Name is required")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 100 characters")]
        public string FirstName { get; set; }

        // Nazwisko właściciela
        [Required(ErrorMessage = "Plot Owner Surname is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Surname should be between 2 and 100 characters")]
        public string Surname { get; set; }

        // Tablica z danymi kontatkowymi właściciela
        [ForeignKey("ContactData")]
        public int ContactDataId { get; set; }
        public ContactData? OwnerContactData { get; set; }

        // Opcjonalnie, do Właściciela może być przypisane Id Użytkownika aplikacji
        public int? UserId { get; set; }
        public User? user { get; set; }
    }
}
