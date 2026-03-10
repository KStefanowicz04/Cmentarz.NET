using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    // Nieboszczyk
    public class Deceased
    {
        // ID nieboszczyka; klucz główny
        [Key]
        public int Id { get; set; }

        // Imię nieboszczyka
        [Required(ErrorMessage = "Deceased Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 100 characters")]
        public string FirstName { get; set; }

        // Nazwisko nieboszczyka
        [Required(ErrorMessage = "Deceased Surname is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 100 characters")]
        public string Surname { get; set; }

        // Data urodzenia nieboszczyka
        [Required(ErrorMessage = "Deceased BirthDate is required")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        // Data śmierci nieboszczyka
        [Required(ErrorMessage = "Deceased DeathDate is required")]
        [DataType(DataType.Date)]
        public DateTime DeathDate { get; set; }
    }
}
