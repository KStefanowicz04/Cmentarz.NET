using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    public class FuneralHome
    {
        //Id domu pogrzebowego; klucz głóny 
        [Key]
        public int Id { get; set; }

        //Nazwa domu pogrzebowego 
        [Required(ErrorMessage ="Funeral home name is required")]
        public string Name { get; set; }

        //Adres siedziby 
        [Required(ErrorMessage = "Address is required")]
        [StringLength(200)]
        public string Address { get; set; }

        //Numer telefonu kontaktowego 
        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        //Email kontaktowy 
        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        //LIsta pogrzebów organizowanych przez ten dom pogrzebowy 
        public ICollection<Funeral>? Funerals { get; set; }
    }
}
