using System.Net.Http.Json;
using System.Threading.Tasks;
using ZooCore.Models;

namespace ZooBlazor.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiRoute;

        public AnimalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseApiRoute = "/api/animals"; 
        }

        public async Task<List<Animal>> GetAll()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Animal>>(_baseApiRoute);
            return result!;
        }

        public async Task<Animal?> Get(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<Animal>(_baseApiRoute + $"/{id}");
            return result;
        }

        public async Task<bool> Post(Animal animal)
        {
            var result = await _httpClient.PostAsJsonAsync(_baseApiRoute, animal);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> Put(Animal animal)
        {
            var result = await _httpClient.PutAsJsonAsync(_baseApiRoute + $"/{animal.Id}", animal);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _httpClient.DeleteAsync(_baseApiRoute + $"/{id}");
            return result.IsSuccessStatusCode;
        }
    }
}
