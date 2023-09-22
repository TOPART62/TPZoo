using System.ComponentModel.DataAnnotations;
using ZooCore.Models;

namespace ZooBlazor.DTOs
{
    public class AnimalEditDTO
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Le Nom est requis.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "L'Espece est requise.")]
        public Espece Espece { get; set; } = Espece.Nothing;
        [Required(ErrorMessage = "L'Age est requis.")]
        public int Age { get; set; }
        [Required(ErrorMessage = "La description est requis.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Le lien vers l'image est requis.")]

        public string? ImageURL { get; set; }
       
        
    }
}
