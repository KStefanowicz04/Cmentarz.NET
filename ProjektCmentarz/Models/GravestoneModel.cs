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
        [Display(Name = "Data postawienia nagrobka")]
        [DataType(DataType.Date)]
        public DateTime InstallationDate { get; set; }

        // Materiał z którego wykonany jest nagrobek; klucz obcy
        [Display(Name = "Materiał")]
        [ForeignKey("Material")]
        [Required(ErrorMessage = "Gravestone material is required")]
        public int MaterialId { get; set; }
        [Display(Name = "Materiał")]
        public Material? Material { get; set; }

        // Grób do którego jest przypisany nagrobek 
        [Display(Name = "Grób")]
        [ForeignKey("Grave")]
        [Required(ErrorMessage = "Gravestone must belong to a Grave")]
        public int GraveId { get; set; }
        [Display(Name = "Grób")]
        public Grave? Grave { get; set; }
    }
}
