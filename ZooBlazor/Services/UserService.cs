using System.Net;
using System.Net.Http.Json;
using System.Security.AccessControl;
using ZooBlazor.DTOs;
using ZooCore.Models;

namespace ZooBlazor.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiRoute;

        public UserService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseApiRoute = configuration["UserAPIUrlHttps"] + "/api/Authentification";
        }

        public async Task<bool> Register(User user)
        {
            var result = await _httpClient.PostAsJsonAsync(_baseApiRoute + $"/Register", user);
            return result.IsSuccessStatusCode;
        }

        public async Task<string> Login(User user)
        {
            var result = await _httpClient.PostAsJsonAsync(_baseApiRoute + $"/Login", user);
            var dto = await result.Content.ReadFromJsonAsync<UserLoginDTO>();
            return dto.Token;
        }
    }
}
