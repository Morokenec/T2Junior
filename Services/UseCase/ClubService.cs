using MauiApp1.Models.ClubModels.Club;
using MauiApp1.Models.ClubModels.ClubList;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase.Interface;
using System.Diagnostics;

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
                string url = $"{AppSetings.base_url}/api/Clubs/by-user/{AppSetings.test_user_guid}";

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

        public async Task<List<Club>> GetClubById()
        {
            try
            {
                string url = $"{AppSetings.base_url}/api/Clubs/{AppSetings.test_club_guid}/profile";
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                string responseContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"[DEBUG] Response: {responseContent}");

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"[ERROR] HTTP {response.StatusCode}: {responseContent}");
                    return null;
                }

                return _jsonDeserializerService.Deserialize<List<Club
                    >>(responseContent);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception: {ex.Message}");
                return null;
            }
        }
    }
}
