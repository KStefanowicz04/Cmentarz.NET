using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjektCmentarz.Models
{
    // ???
    public class Reservation
    {
        // Id rezerwacji; klucz główny 
        [Key]
        public int Id { get; set; }

        // Data rozpoczęcia rezerwacji 
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }


        // Data zakończenia rezerwacji 
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        // Klucz obcy do działi 
        [ForeignKey("Plot")]
        [Required]
        public int PlotId { get; set; }

        // Działka która została zarezerwowana 
        public Plot Plot {  get; set; }

        // Imie i nazwisko osoby rezerwującej 
        [Required]
        [StringLength(100)]
        public string ReservedBy { get; set; }
    }
}
