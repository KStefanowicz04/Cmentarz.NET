using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektCmentarz.Models
{
    // Sekcja cmentarza - zawiera konkretne działki z grobami
    public class GraveyardSection
    {
        // ID sekcji; klucz główny
        [Key]
        public int Id { get; set; }

        // Nazwa sekcji; klucz obcy na encję słownikową
        [ForeignKey("SectionType")]
        [Required(ErrorMessage = "Section type is required")]
        public int SectionTypeId { get; set; }
        public SectionType SectionType { get; set; }

        // Lista działek w danej sekcji; może być pusta - sekcja może nie zawierać żadnych działek
        public ICollection<Plot>? Plots { get; set; }
    }
}
