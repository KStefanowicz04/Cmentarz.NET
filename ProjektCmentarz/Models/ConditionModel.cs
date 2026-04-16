using System;
using System.ComponentModel.DataAnnotations;

namespace ProjektCmentarz.Models
{
    // Stan techniczny nagrobka np. dobry, do renowacji, uszkodzony
    public class Condition
    {
        // ID stanu technicznego; klucz główny 
        [Key]
        public int Id { get; set; }

        // Stan techniczny
        [Display(Name = "Stan techniczny")]
        [Required(ErrorMessage = "Stan techniczny jest wymagany")]
        [StringLength(100)]
        public string ConditionType { get; set; }
    }
}
