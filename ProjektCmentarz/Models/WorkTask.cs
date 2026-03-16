using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    public class WorkTask
    {
        [Key]
        public int Id { get; set; }

        // Opis zadania do wykonania
        [Required(ErrorMessage = "Task description is required")]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        // Status zadania 
        [Required]
        public string Status { get; set; } = "New";

        // Termin wykonania
        [Required]
        public DateTime DueDate { get; set; }

        // Grabarz, do którego przypisane jest zadanie
        [Required]
        public int GravekeeperId { get; set; }

        [ForeignKey("GravekeeperId")]
        public virtual Gravekeeper Gravekeeper { get; set; } = null!;

        //Pogrzeb, do którego przypisane jest zadanie
         public int? FuneralId { get; set; }

        [ForeignKey("FuneralId")]
        public virtual Funeral? Funeral { get; set; }
    }
}
