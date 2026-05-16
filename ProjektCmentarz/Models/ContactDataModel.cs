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
        [Display(Name = "Numer Telefonu")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Phone number should be between 1 and 20 characters")]
        public string? PhoneNumber { get; set; }

        // Email
        [Display(Name = "Email")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Phone number should be between 3 and 40 characters")]
        public string? EMail { get; set; }

        // Adres
        // Miasto
        [Display(Name = "Miasto")]
        public string? CityName { get; set; }

        // Ulica
        [Display(Name = "Ulica")]
        public string? StreetName { get; set; }

        // Kod pocztowy
        [Display(Name = "Kod Pocztowy")]
        public string? ZipCode { get; set; }
    }
}
