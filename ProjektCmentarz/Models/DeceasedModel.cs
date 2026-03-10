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
    //Grob
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
    // Pogrzeb
    //2 zmarly
    public class Funeral
{
        [Key]
        public int Id { get; set; }
    
        // Data pogrzebu
        public DateTime FuneralDate { get; set; }
    
        // Miejsce ceremonii
        public string CeremonyPlace { get; set; }
    
        // Klucz obcy do nieboszczyka
        public int DeceasedId { get; set; }
    
        public Deceased Deceased { get; set; }
    }
}
