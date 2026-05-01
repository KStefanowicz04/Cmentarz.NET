using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    // Model służący do zmiany hasła Użytkownika
    public class UserEditPassword
    {
        // ID użytkownika; klucz główny
        [Key]
        public int UserId { get; set; }

        // Obecne hasło
        [Display(Name = "Obecne hasło")]
        [Required(ErrorMessage = "Obecne hasło jest wymagane")]
        public string OldPassword { get; set; }

        // Nowe hasło
        [Display(Name = "Nowe hasło")]
        [Required(ErrorMessage = "Nowe hasło jest wymagane")]
        public string NewPassword { get; set; }
    }
}
