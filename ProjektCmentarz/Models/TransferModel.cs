using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    // Przeniesienie nieboszczyka między grobami
    public class Transfer
    {
        // ID przeniesienia; klucz główny
        [Key]
        public int Id { get; set; }

        // Data odbycia przeniesienia
        [Required(ErrorMessage = "Transfer Date is required")]
        [DataType(DataType.Date)]
        public DateTime TransferDate { get; set; }

        // Klucz obcy do nieboszczyka danego przeniesienia.
        [ForeignKey("Deceased")]
        [Required(ErrorMessage = "A transferred Deceased must be included")]
        public int DeceasedId { get; set; }
        public Deceased Deceased { get; set; }

        // Pierwotny grób; klucz obcy
        [Required(ErrorMessage = "Start Grave is required")]
        public int FromGraveId { get; set; }
        public Grave TransferFromGrave { get; set; }

        // Końcowy grób; klucz obcy
        [Required(ErrorMessage = "End Grave is required")]
        public int ToGraveId { get; set; }
        public Grave TransferToGrave { get; set; }

        // Lista Grabarzy wykonujących przeniesienie
        public ICollection<Gravekeeper>? TransferGravekeepers { get; set; }
    }
}
