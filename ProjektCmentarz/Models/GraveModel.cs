using System.ComponentModel.DataAnnotations;

namespace ProjektCmentarz.Models
{
    // Grób (to miejsce w cmetarzu)
    public class Grave
    {
        // ID grobu; klucz główny
        [Key]
        public int Id { get; set; }

        // Działka na której jest dany grób; grób może należeć tylko do 1 działki
        public Plot GravePlot { get; set; }

        // Relacja ze zmarłym (Jeśli wiecej niż jeden, jest to grób rodzinny)
        public List<Deceased> Deceased { get; set; }
    }
}
