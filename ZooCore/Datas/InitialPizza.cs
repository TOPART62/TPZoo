using ZooCore.Models;

namespace ZooCore.Datas
{
    public static class InitialAnimal
    {
        public static readonly List<Animal> animals = new List<Animal>()
        {
            new Animal{ Id =1, Name ="Bacon", Espece= Espece.Elephant, Age = 30, Description = "Elephant",ImageURL = "/images/bacon.jpg"  },
            new Animal{ Id =2, Name ="Lardon", Espece= Espece.Tigre, Age = 10, Description = "Tigre Lardon",ImageURL = "/images/bacon.jpg"  },
            new Animal{ Id =3, Name ="Jambon", Espece= Espece.Dromadaire, Age = 15, Description = "Dromadaire Jambon",ImageURL = "/images/bacon.jpg"  },
            new Animal{ Id =4, Name ="Saucisson", Espece= Espece.Serpent, Age = 20, Description = "Serpent Saucisson",ImageURL = "/images/bacon.jpg"  },
        };
    }
}
