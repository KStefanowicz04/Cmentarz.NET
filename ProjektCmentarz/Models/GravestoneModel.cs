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

        // Data postawienia nagrobka 
        [DataType(DataType.Date)]
        public DateTime InstallationDate { get; set; }

        // Stan techniczny nagrobka; klucz obcy
        [ForeignKey("Condition")]
        [Required(ErrorMessage = "Gravestone condition is required")]
        public int ConditionId { get; set; }
        public Condition Condition { get; set; }

        // Materiał z którego wykonany jest nagrobek; klucz obcy
        [ForeignKey("Material")]
        [Required(ErrorMessage = "Gravestone material is required")]
        public int MaterialId { get; set; }
        public Material Material { get; set; }

        // Klucz obcy do grobu, na którym znajduje się nagrobek 
        [ForeignKey("Grave")]
        [Required(ErrorMessage = "Gravestone must belong to a Grave")]
        public int GraveId { get; set; }
        // Grób do którego jest przypisany nagrobek 
        public Grave Grave { get; set; }
    }
}
