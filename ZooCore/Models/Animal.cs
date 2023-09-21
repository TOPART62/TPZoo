using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooCore.Models
{
    public enum Espece
    {
        Nothing,
        Lion,
        Panthere,
        Elephant,
        Dromadaire,
        Tigre,
        Serpent,
        FormateurM2i
    }
    public class Animal
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z].*", ErrorMessage = "Name must start with an Uppercase Letter !")]
        public string? Name { get; set; }
        [Required]
        public Espece Espece { get; set; } = Espece.Nothing;    
        public int Age { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
    }
}
