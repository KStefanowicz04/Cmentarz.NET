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
        [Display(Name = "Data Pogrzebu")]
        [Required(ErrorMessage = "Funeral Date is required")]
        [DataType(DataType.Date)]
        public DateTime FuneralDate { get; set; }

        // Miejsce odbycia pogrzebu (działka); klucz obcy
        [Display(Name = "Działka")]
        [ForeignKey("Plot")]
        [Required(ErrorMessage = "A Funeral requires a place")]
        public int PlotId { get; set; }
        [Display(Name = "Działka")]
        public Plot FuneralPlot { get; set; }

        // Klucz obcy do nieboszczyka danego pogrzebu. Nieboszczyk jest nadrzędny!
        [Display(Name = "Nieboszczyk")]
        [ForeignKey("Deceased")]
        [Required(ErrorMessage = "A Funeral requires a Deceased")]
        public int DeceasedId { get; set; }
        [Display(Name = "Nieboszczyk")]
        public Deceased Deceased { get; set; }

        // Klucz obcy na Księdza, który wykonał dany pogrzeb
        [Display(Name = "Ksiądz")]
        [ForeignKey("Priest")]
        public int PriestId { get; set; }
        [Display(Name = "Ksiądz")]
        public Priest? FuneralPriest { get; set; }

        // Lista Grabarzy biorących udział w pogrzebie; może być pusta
        public ICollection<Gravekeeper>? FuneralGravekeepers { get; set; }
    }
}
