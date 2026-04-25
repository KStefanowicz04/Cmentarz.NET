using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    // Model służący do logowania
    public class LoginModel
    {
        // Email
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email jest wymagany")]
        public string Email { get; set; }

        // Hasło
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password jest wymagane")]
        public string Password { get; set; }
    }
}
