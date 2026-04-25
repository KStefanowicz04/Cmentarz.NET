using System.ComponentModel.DataAnnotations;

namespace ProjektCmentarz.Models
{
    // Parafia
    public class Parish
    {
        // ID parafii; klucz główny 
        [Key]
        public int Id { get; set; }

        // Nazwa parafii
        [Display(Name = "Nazwa Parafii")]
        [Required(ErrorMessage = "Parish name is requires")]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
