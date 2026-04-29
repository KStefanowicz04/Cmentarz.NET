using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    // Rola użytkownika
    public class Role
    {
        // ID Roli; klucz główny
        [Key]
        public int Id { get; set; }

        // Nazwa roli
        [Display(Name = "Rola")]
        [Required(ErrorMessage = "Nazwa roli jest wymagana.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nazwa roli ma od 1 do 100 znaków.")]
        public string RoleName { get; set; }

        // Użytkownicy przypisani do danej roli
        public IEnumerable<User> Users { get; set; } = new List<User>();
    }
}
