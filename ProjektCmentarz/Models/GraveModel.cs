using System.ComponentModel.DataAnnotations;

namespace ProjektCmentarz.Models
{
    public class GraveModel
    {
        // Grób
        public class Grave
        {
            // ID grobu; klucz główny
            [Key]
            public int Id { get; set; }

            // Numer grobu
            public string GraveNumber { get; set; }

            // Sektor cmentarza - może to powinna być encja słownikowa?
            public string Sector { get; set; }

            // Relacja ze zmarłym (Wiecej niż jeden grób rodzinny)
            public List<Deceased> Deceased { get; set; }
        }
    }
}
