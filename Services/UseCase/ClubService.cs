using MauiApp1.Models.ClubModels.Club;
using MauiApp1.Models.ClubModels.ClubList;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase.Interface;
using System.Diagnostics;
using System.Text.Json;
using System.Text;

namespace MauiApp1.Services.UseCase
{
    public class ClubService : IClubService
    {
        private readonly HttpClient _httpClient;
        private readonly IJsonDeserializerService _jsonDeserializerService;


        public ClubService(HttpClient httpClient, IJsonDeserializerService jsonDeserializerService)
        {
            _httpClient = httpClient;
            _jsonDeserializerService = jsonDeserializerService;
        }

        public async Task<List<ClubList>> GetClubsAsync()
        {
            try
            {
                string url = $"{AppSettings.base_url}/api/Clubs/by-user/{AppSettings.test_user_guid}";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                string responseContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"[DEBUG] Response: {responseContent}");

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"[ERROR] HTTP {response.StatusCode}: {responseContent}");
                    return null;
                }

                // Десериализуем ответ в список клубов
                return _jsonDeserializerService.Deserialize<List<ClubList>>(responseContent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<Club> GetClubById(Guid clubId)
        {
            try
            {
                string url = $"{AppSettings.base_url}/api/Clubs/{clubId.ToString()}/profile";
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                string responseContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"[DEBUG] Response: {responseContent}");

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"[ERROR] HTTP {response.StatusCode}: {responseContent}");
                    return null;
                }

                // Десериализация в объект Club
                return _jsonDeserializerService.Deserialize<Club>(responseContent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception: {ex.Message}");
                return null;
            }
        }

        public async Task SubscribeClub(Guid clubId, Guid userId, Guid roleId)
        {
            string url = $"{AppSettings.base_url}/api/Clubs/{clubId}/add-user";

            var requestBody = new
            {
                userId = userId.ToString(),
                roleId = roleId.ToString()
            };

            using var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json");

            using var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
        }
    }
}
