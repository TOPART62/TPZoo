using System.Net;
using System.Net.Http.Json;
using System.Security.AccessControl;
using ZooBlazor.DTOs;
using ZooCore.DTOs;
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
            _baseApiRoute = configuration["APIUrlHttp"] + "/api/Authentication";
        }

        public async Task<bool> Register(User user)
        {
            var result = await _httpClient.PostAsJsonAsync(_baseApiRoute + $"/Register", user);
            return result.IsSuccessStatusCode;
        }

        public async Task<string> Login(LoginRequestDTO user)
        {
            var result = await _httpClient.PostAsJsonAsync(_baseApiRoute + $"/Login", user);
            var dto = await result.Content.ReadFromJsonAsync<UserLoginDTO>();
            if(dto is null)
            {
                return "Invalid Authentication!";
            }
            return dto.Token;
        }
    }
}
