using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    // Nieboszczyk
    public class Deceased
    {
        // ID nieboszczyka; klucz główny
        [Key]
        public int Id { get; set; }

        // Imię nieboszczyka
        public string FirstName { get; set; }

        // Nazwisko nieboszczyka
        public string Surname { get; set; }

        // Data urodzenia nieboszczyka
        public DateTime BirthDate { get; set; }

        // Data śmierci nieboszczyka
        public DateTime DeathDate { get; set; }
    }
        //grob
        public class Grave
    {
        [Key]
        public int Id { get; set; }
    
        // Numer grobu
        public string GraveNumber { get; set; }
    
        // Sektor cmentarza
        public string Sector { get; set; }
    
        // Relacja ze zmarłym (Wiecej niz jeden grób rodzinny)
        public List<Deceased> Deceased { get; set; }
    }
}
