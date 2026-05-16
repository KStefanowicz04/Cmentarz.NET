using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    // Model służący do edytowania własnych danych użytkownika przez użytkownika
    public class UserProfileEdit
    {
        // ID użytkownika; klucz główny
        [Key]
        public int UserId { get; set; }

        // Podstawowe dane użytkownika z User
        // Imię użytkownika
        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Imię użytkownika jest wymagane.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Imię ma od 1 do 100 znaków.")]
        public string FirstName { get; set; }

        // Nazwisko użytkownika
        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nazwisko ma od 1 do 100 znaków.")]
        public string Surname { get; set; }

        // Email użytkownika
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email jest wymagany.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Email ma od 1 do 100 znaków.")]
        public string Email { get; set; }


        // Dane kontaktowe z ContactData
        [Display(Name = "Numer Telefonu")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Phone number should be between 1 and 20 characters")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Email Kontaktowy")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Contact Email should be between 3 and 40 characters")]
        public string? ContactEMail { get; set; }

        [Display(Name = "Miasto")]
        public string? CityName { get; set; }

        [Display(Name = "Ulica")]
        public string? StreetName { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string? ZipCode { get; set; }
    }
}
