using System.ComponentModel.DataAnnotations;

namespace ProjektCmentarz.Models
{
    // Sekcja cmentarza - zawiera konkretne działki z grobami
    public class Section
    {
        // ID sekcji; klucz główny
        [Key]
        public int Id { get; set; }

        // Nazwa sekcji
        [Required(ErrorMessage = "Section Name is required")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Section Name should be between 2 and 100 characters")]
        public string Name { get; set; }

        // Lista działek w danej sekcji; może być pusta - sekcja może nie zawierać żadnych działek
        public List<Plot>? Plots { get; set; }
    }
}
