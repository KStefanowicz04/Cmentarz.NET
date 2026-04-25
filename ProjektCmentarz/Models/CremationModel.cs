using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    public class Cremation
    {
        // Id kremacji; klucz główny 
        public int Id { get; set; }

        // Data kremacji
        [Display(Name = "Data kremacji")]
        [Required(ErrorMessage = "Data kremacji jest wymagana")]
        [DataType(DataType.Date)]
        public DateTime CremationDate { get; set; }

        // Klucz obcy do zmarłego, którego dotyczy kremacja
        [Display(Name = "Nieboszczyk")]
        [ForeignKey("Deceased")]
        [Required(ErrorMessage = "Kremacja wymaga Nieboszczyka, którego dotyczy")]
        public int DeceasedId { get; set; }
        // Zmarły, którego dotyczy kremacja 
        public Deceased? Deceased { get; set; }
    }
}
