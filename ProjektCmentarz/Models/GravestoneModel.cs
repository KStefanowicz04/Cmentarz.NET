using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjektCmentarz.Models


{
    public class Gravestone
    {
        // Id nagrobka; klucz główny 
        [Key]
        public int Id { get; set; }

        // Materiał z którego wykonany jest nagrobek 
        [Required(ErrorMessage = "Gravestone material is required")]
        public string Material { get; set; }

        // Data postawienia nagrobka 
        [DataType(DataType.Date)]
        public DateTime InstallationDate { get; set; }

        // Stan techniczny nagrobka np. dobry, do renowacji, uszkodzony
        [StringLength(100)]
        public string? Condition { get; set; }

        // Klucz obcy do grobu, na którym znajduje się nagrobek 
        [ForeignKey("Grave")]
        [Required(ErrorMessage = "Gravestone must belong to a Grave")]
        public int GraveId { get; set; }
        // Grób do którego jest przypisany nagrobek 
        public Grave Grave { get; set; }
    }
}
