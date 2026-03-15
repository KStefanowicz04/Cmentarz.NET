using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    //Wniosek o konserwację lub naprawę pomnika 
    public class MaintenanceRequest
    {
        [Key]
        public int Id { get; set; }

        //Opis zgłoszenia 
        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        //Data zgłoszenia 
        [Required]
        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; }

        //Klucz obcy do grobu, którego dotyczny wnioisek 
        [ForeignKey("Grave")]
        [Required]
        public int GraveId { get; set; }

        //Grób do którego przypisany jest wniosek 
        public Grave Grave { get; set; }
    }
}
