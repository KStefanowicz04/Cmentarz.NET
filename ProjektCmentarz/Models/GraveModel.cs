using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    // Grób (to miejsce w cmetarzu)
    public class Grave
    {
        // ID grobu; klucz główny
        [Key]
        public int Id { get; set; }

        // Działka na której jest dany grób; grób może należeć tylko do 1 działki
        [ForeignKey("Plot")]
        public int PlotId { get; set; }
        public Plot GravePlot { get; set; }

        // Relacja ze zmarłym w danym grobie
        [ForeignKey("Deceased")]
        public int DeceasedId { get; set; }
        public Deceased GraveDeceased { get; set; }

        // Głębokość pochówku; klucz obcy encji słownikowej
        [ForeignKey("BurialDepth")]
        public int BurialDepthId { get; set; }
        public BurialDepth BurialDepth { get; set; }
    }
}
