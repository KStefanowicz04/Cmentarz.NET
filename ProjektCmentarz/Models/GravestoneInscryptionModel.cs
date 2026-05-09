using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    // Opis na nagrobku
    public class GravestoneInscryption
    {
        // ID opisu; klucz główny
        [Key]
        public int Id { get; set; }

        // Opis
        [Required(ErrorMessage = "Gravestone Inscryption is required")]
        public string Inscryption { get; set; }
    }
}
