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

        // Klucz obcy do nieboszczyka danego pogrzebu. Nieboszczyk jest nadrzędny!
        [ForeignKey("Deceased")]
        [Required(ErrorMessage = "A Funeral requires a Deceased")]
        public int DeceasedId { get; set; }
        public Deceased Deceased { get; set; }

        // Klucz obcy na Księdza, który wykonał dany pogrzeb
        [ForeignKey("Priest")]
        public int PriestId { get; set; }
        public Priest FuneralPriest { get; set; }

        // Lista Grabarzy biorących udział w pogrzebie; może być pusta
        public ICollection<Gravekeeper>? FuneralGravekeepers { get; set; }
    }
}
