using ZooCore.Models;

namespace ZooBlazor.Services
{
    public class AnimalService
    {
        private List<Animal> _animals; 

        public AnimalService()
        {
           
            _animals = new List<Animal>
            {
            new Animal{ Id =1, Name ="Bacon", Espece= Espece.Elephant, Age = 30, Description = "Elephant",ImageURL = "/images/bacon.jpg"  },
            new Animal{ Id =2, Name ="Lardon", Espece= Espece.Tigre, Age = 10, Description = "Tigre Lardon",ImageURL = "/images/bacon.jpg"  },
            new Animal{ Id =3, Name ="Jambon", Espece= Espece.Dromadaire, Age = 15, Description = "Dromadaire Jambon",ImageURL = "/images/bacon.jpg"  },
            new Animal{ Id =4, Name ="Saucisson", Espece= Espece.Serpent, Age = 20, Description = "Serpent Saucisson",ImageURL = "/images/bacon.jpg"  },
            };
        }

        public List<Animal> GetAnimals()
        {
            return _animals;
        }

        public Animal GetAnimalById(int id)
        {
            return _animals.FirstOrDefault(a => a.Id == id);
        }

        public void AddAnimal(Animal animal)
        {
            animal.Id = _animals.Max(a => a.Id) + 1;
            _animals.Add(animal);
        }

        public void UpdateAnimal(Animal updatedAnimal)
        {
            var existingAnimal = _animals.FirstOrDefault(a => a.Id == updatedAnimal.Id);
            if (existingAnimal != null)
            {
                existingAnimal.Name = updatedAnimal.Name;
                existingAnimal.Espece = updatedAnimal.Espece;
                existingAnimal.Age = updatedAnimal.Age;
                existingAnimal.Description = updatedAnimal.Description;
                existingAnimal.ImageURL = updatedAnimal.ImageURL;
            }
        }

        public void DeleteAnimal(int id)
        {
            var animalToDelete = _animals.FirstOrDefault(a => a.Id == id);
            if (animalToDelete != null)
            {
                _animals.Remove(animalToDelete);
            }
        }
    }
}
