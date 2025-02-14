using MauiApp1.Models.Profile;
using MauiApp1.Services.AppHelper;
using System.Diagnostics;
using System.Text.Json;

namespace MauiApp1.Services.UseCase
{
    public class TaiyoService : ITaiyoService
    {
        private readonly HttpClient _httpClient;
        private readonly IJsonDeserializerService _jsonDeserializerService;

        public TaiyoService(HttpClient httpClient, IJsonDeserializerService jsonDeserializerService)
        {
            _httpClient = httpClient;
            _jsonDeserializerService = jsonDeserializerService;
        }

        public async Task<TaiyoResponse> GetTaiyoDataAsync()
        {
            try
            {
                string url = "hahatun.fun:5138/api/Account/profile/5431ce17-ffa3-4297-a523-0e111f329842"; // Замените на реальный URL

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                string responseContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"[DEBUG] Response: {responseContent}"); // Лог в консоль

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"[ERROR] HTTP {response.StatusCode}: {responseContent}");
                    return null;
                }

                return JsonSerializer.Deserialize<TaiyoResponse>(responseContent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception: {ex.Message}");
                return null;
            }
        }
    }
}
