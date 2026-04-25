using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    // Użytkownik
    public class User
    {
        // ID użytkownika; klucz główny
        [Key]
        public int UserId { get; set; }

        // Imię użytkownika
        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Imię użytkownika jest wymagana.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Imię ma od 1 do 100 znaków.")]
        public string FirstName { get; set; }

        // Nazwisko użytkownika
        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Nazwisko jest wymagana.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nazwisko ma od 1 do 100 znaków.")]
        public string Surname { get; set; }

        // Email użytkownika
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email jest wymagany.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Email ma od 1 do 100 znaków.")]
        public string Email { get; set; }

        // Zahashowane hasło
        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Hasło jest wymagane")]
        public byte[] Password { get; set; }
    }
}
