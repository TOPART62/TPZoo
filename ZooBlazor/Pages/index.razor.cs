using Microsoft.AspNetCore.Components;
using ZooCore.Models;
using ZooBlazor.DTOs;
using ZooBlazor.Services;
using System.Text.RegularExpressions;

namespace ZooBlazor.Pages
{
    public partial class index
    {
#nullable disable
        [Inject]
        public IAnimalService AnimalService { get; set; }
#nullable enable
        private string? LoadingMessage { get; set; }
        private bool IsAdminMode { get; set; } = false;
        private Dictionary<Animal, int> Cart { get; set; } = new Dictionary<Animal, int>();

        private List<Animal> AnimalList { get; set; } = new();
        private AnimalEditDTO? AnimalToEdit { get; set; } = null;
        private EditionModes EditionMode { get; set; }
        private enum EditionModes
        {
            Post,
            Put
        }
        protected override async Task OnInitializedAsync()
        {
            LoadingMessage = "Récupération des Animaux...";
            AnimalList = await AnimalService.GetAll();
            LoadingMessage = "";
        }

        private void AddToCart(Animal animal)
        {
            if (Cart.ContainsKey(animal))
                Cart[animal]++;

        }

        private void EditAnimal(Animal animal)
        {
            AnimalToEdit = new AnimalEditDTO()
            {
                Id = animal.Id,
                Name = animal.Name,
                Espece = animal.Espece,
                Age = animal.Age,
                Description  = animal.Description,
                ImageURL = Regex.Split(animal.ImageURL!, @"https:\/\/localhost:\d{1,4}").Last() 
            };
            EditionMode = EditionModes.Put;
        }

        private void AddAnimal()
        {
            AnimalToEdit = new AnimalEditDTO();
            EditionMode = EditionModes.Post;
        }
        private async Task DeleteAnimal(int id)
        {
            AnimalList.RemoveAll(p => p.Id == id);
            await AnimalService.Delete(id);
        }

        private async Task SubmitAnimal()
        {
            switch (EditionMode)
            {
                case EditionModes.Post:
                    var animal2 = new Animal()
                    {
                        Name = AnimalToEdit!.Name,
                        Espece = AnimalToEdit.Espece,                       
                        Age = AnimalToEdit.Age!,
                        Description = AnimalToEdit.Description,
                        ImageURL = Regex.Split(AnimalToEdit.ImageURL!, @"https:\/\/localhost:\d{1,4}").Last(),
                    };
                    AnimalList.Add(animal2);
                    await AnimalService.Post(animal2);
                    break;
                case EditionModes.Put:
                    var animal = AnimalList.Find(animal => animal.Id == AnimalToEdit!.Id)!;
                    animal.Name = AnimalToEdit!.Name;
                    animal.Espece = AnimalToEdit.Espece;
                    animal.Age = AnimalToEdit.Age;
                    animal.Description = AnimalToEdit.Description;
                    animal.ImageURL = Regex.Split(AnimalToEdit.ImageURL!, @"https:\/\/localhost:\d{1,4}").Last();
               
                    await AnimalService.Put(animal);
                    break;
                default:
                    break;
            }

            AnimalToEdit = null;
        }
    }
}
