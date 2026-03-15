using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    public class FuneralHome
    {
        // Id domu pogrzebowego; klucz główny 
        [Key]
        public int Id { get; set; }

        // Nazwa domu pogrzebowego 
        [Required(ErrorMessage ="Funeral home name is required")]
        public string Name { get; set; }

        // Dane kontaktowe
        public ContactData GravekeeperContactData { get; set; }

        // Lista pogrzebów organizowanych przez ten dom pogrzebowy; opcjonalne, dany zakład może nie organizował pogrzebów
        public ICollection<Funeral>? Funerals { get; set; }
    }
}
