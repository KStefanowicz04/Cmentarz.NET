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
        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Deceased Name is required")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 100 characters")]
        public string FirstName { get; set; }

        // Nazwisko nieboszczyka
        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Deceased Surname is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Surname should be between 2 and 100 characters")]
        public string Surname { get; set; }

        // Data urodzenia nieboszczyka
        [Display(Name = "Data Urodzenia")]
        [Required(ErrorMessage = "Deceased BirthDate is required")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        // Data śmierci nieboszczyka
        [Display(Name = "Data Zgonu")]
        [Required(ErrorMessage = "Deceased DeathDate is required")]
        [DataType(DataType.Date)]
        public DateTime DeathDate { get; set; }

        // Klucz obcy na Trumnę należącą do Nieboszczyka. Nieboszczyk jest nadrzędny!
        public int? CasketId { get; set; }
        public Casket? Casket { get; set; }

        // Klucz obcy na Pogrzeb, w którym brał udział Nieboszczyk. Nieboszczyk jest nadrzędny!
        public int? FuneralId { get; set; }
        public Funeral? Funeral { get; set; }
    }
}
