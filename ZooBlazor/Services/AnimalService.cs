using Blazored.LocalStorage;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZooCore.Models;

namespace ZooBlazor.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiRoute;
        private readonly ILocalStorageService _localStorage;

        public AnimalService(HttpClient httpClient, IConfiguration configuration, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _baseApiRoute = configuration["APIUrlHttp"] + "/api/animals";
            _localStorage = localStorage;
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
            var stringAnimal = JsonConvert.SerializeObject(animal);
            var request = new HttpRequestMessage(HttpMethod.Post, _baseApiRoute);
            request.Content = new StringContent(stringAnimal, Encoding.UTF8, "application/json");
            return await SendRequest(request);
        }

        public async Task<bool> Put(Animal animal)
        {
            var stringAnimal = JsonConvert.SerializeObject(animal);
            var request = new HttpRequestMessage(HttpMethod.Put, _baseApiRoute);
            request.Content = new StringContent(stringAnimal, Encoding.UTF8, "application/json");
            return await SendRequest(request);
        }

        public async Task<bool> Delete(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, _baseApiRoute + $"/{id}");
            return await SendRequest(request);
        }

        public async Task<bool> SendRequest(HttpRequestMessage request)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await GetJWT());
            var result = await _httpClient.SendAsync(request);
            return result.IsSuccessStatusCode;
        }

        private async Task<string> GetJWT()
        {
            return await _localStorage.GetItemAsync<string>("jwt");
        }
    }
}
