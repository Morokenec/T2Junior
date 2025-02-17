using MauiApp1.Models;
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
                // Замените URL на реальный адрес вашего API
                string url = "https://t2.hahatun.fun/api/Clubs/by-user/5431ce17-ffa3-4297-a523-0e111f329842";

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
    }
}
