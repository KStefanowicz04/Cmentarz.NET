using System.ComponentModel.DataAnnotations;

namespace ProjektCmentarz.Models
{
    // Tabela zawierająca dane kontaktowe osób
    public class ContactData
    {
        // ID danych kontaktowych; klucz główny
        [Key]
        public int Id { get; set; }

        // Numer telefonu
        [Required(ErrorMessage = "Person phone number is required")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Phone number should be between 1 and 20 characters")]
        public string PhoneNumber { get; set; }

        // Email
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Phone number should be between 3 and 40 characters")]
        public string EMail { get; set; }

        // Adres
        // Miasto
        public string CityName { get; set; }

        // Ulica
        public string StreetName { get; set; }

        // Kod pocztowy
        public string ZipCode { get; set; }
    }
}
