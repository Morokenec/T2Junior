using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using T2JuniorMobile.Model.Auth;

namespace T2JuniorMobile.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private const string AuthEndpoint = "http://localhost:5138/api/Account/login"; 

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
           
            var authRequest = new AuthRequest { Email = email, Password = password };
            var response = await _httpClient.PostAsJsonAsync(AuthEndpoint, authRequest);

            if (response.IsSuccessStatusCode)
            {
                var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
                return authResponse.Token;
            }
            return null;
        }
    }
}
