using MauiApp1.Models.Profile;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase.Interface;
using System.Diagnostics;

namespace MauiApp1.Services.UseCase
{
    /// <summary>
    /// Сервис для получения данных профиля пользователя.
    /// </summary>
    /// <remarks>
    /// Предоставление методов для получения информации о профиле пользователя.
    /// </remarks>
    public class ProfileService : IProfileService
    {
        private readonly HttpClient _httpClient;
        private readonly IJsonDeserializerService _jsonDeserializerService;

        /// <summary>
        /// Конструктор сервиса ProfileService.
        /// </summary>
        /// <param name="httpClient">HTTP-клиент для выполнения запросов.</param>
        /// <param name="jsonDeserializerService">Сервис для десериализации JSON.</param>
        public ProfileService(HttpClient httpClient, IJsonDeserializerService jsonDeserializerService)
        {
            _httpClient = httpClient;
            _jsonDeserializerService = jsonDeserializerService;
        }

        /// <summary>
        /// Получение данных профиля пользователя.
        /// </summary>
        /// <returns>Объект ProfileResponse с данными профиля или null в случае ошибки.</returns>
        public async Task<ProfileResponse> GetProfileDataAsync()
        {
            try
            {
                string url = $"{AppSetings.base_url}/api/Account/profile/{AppSetings.test_user_guid}";

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                string responseContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"[DEBUG] Response: {responseContent}");

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"[ERROR] HTTP {response.StatusCode}: {responseContent}");
                    return null;
                }

                return _jsonDeserializerService.Deserialize<ProfileResponse>(responseContent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception: {ex.Message}");
                return null;
            }
        }
    }
}
