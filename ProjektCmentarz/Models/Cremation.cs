using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    public class Cremation
    {
        //Id kremacji; klucz główny 
        public int Id { get; set; }

        //Data kremacji 
        [Required(ErrorMessage ="Cremation date is required")]
        [DataType(DataType.Date)]
        public DateTime CremationDate { get; set; }

        //Klucz obcy do zmarłego, którego dotyczy kremacja 
        [ForeignKey("Deceased")]
        [Required(ErrorMessage = "Cremation must belong to a Descased")]
        public int DescasedId { get; set; }

        //Zmarły, którego dotyczy kremacja 
        public Deceased Deceased { get; set; }
    }
}
