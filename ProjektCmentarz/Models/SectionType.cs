using System.ComponentModel.DataAnnotations;
namespace ProjektCmentarz.Models
{
    //Słownik typów sektora cmentarza 
    public class SectionType
    {
        //ID typu sektora; klucz główny 
        [Key]
        public int Id { get; set; }

        //Nazwa typu sektora 
        [Required(ErrorMessage = "Section type name is requires")]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
