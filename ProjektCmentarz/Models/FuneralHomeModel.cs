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
        [Display(Name = "Nazwa domu pogrzebowego")]
        [Required(ErrorMessage ="Funeral home name is required")]
        public string FuneralHomeName { get; set; }

        // Dane kontaktowe
        [ForeignKey("ContactData")]
        public int ContactDataId { get; set; }
        public ContactData? FuneralHomeContactData { get; set; }

        // Lista pogrzebów organizowanych przez ten dom pogrzebowy;
        // opcjonalne, dany zakład może jeszcze nie organizował pogrzebów
        public ICollection<Funeral>? Funerals { get; set; }
    }
}
