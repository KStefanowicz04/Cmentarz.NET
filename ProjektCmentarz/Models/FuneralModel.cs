using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
     // Pogrzeb
    public class Funeral
    {
        // ID pogrzebu; klucz główny
        [Key]
        public int Id { get; set; }

        // Data odbycia pogrzebu
        [Required(ErrorMessage = "Funeral Date is required")]
        [DataType(DataType.Date)]
        public DateTime FuneralDate { get; set; }

        // Miejsce odbycia pogrzebu - może to powinna być encja słownikowa?
        public string FuneralPlace { get; set; }

        // Klucz obcy do nieboszczyka danego pogrzebu
        [ForeignKey("Deceased")]
        public int DeceasedId { get; set; }
        // "Naigation property"
        public Deceased Deceased { get; set; }
    }
}
