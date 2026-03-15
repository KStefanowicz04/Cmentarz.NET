using System.ComponentModel.DataAnnotations;

namespace ProjektCmentarz.Models
{
    public class GraveType
    {
        // Id typu grobu; klucz głóny 
        [Key]
        public int Id { get; set; }

        //Nazwa typu grobu 
        [Required(ErrorMessage = "Grave type name is required")]
        [StringLength(100)]
        public string Name { get; set; }    
    }
}
