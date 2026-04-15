using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    // Działka - zawiera grób lub groby
    public class Plot
    {
        // ID działki; klucz główny
        [Key]
        public int Id { get; set; }

        // Lista grobów na danej działce; może być pusta, co oznacza brak grobów na danej działce
        public ICollection<Grave>? Graves { get; set; }

        // Osoba do której należy dana działka
        [ForeignKey("PlotOwner")]
        public int PlotOwnerId { get; set; }
        public PlotOwner? Owner { get; set; }
    }
}
