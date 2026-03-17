using System.ComponentModel.DataAnnotations;

namespace ProjektCmentarz.Models
{
    // Stan wykonania zadania (wykonane, niewykonane, zaległe, itp.?)
    public class TaskStatus
    {
        // ID statusu wykonania zadania; klucz główny 
        [Key]
        public int Id { get; set; }

        // Status
        [Required(ErrorMessage = "Task status is required")]
        [StringLength(100)]
        public string Status { get; set; }
    }
}
