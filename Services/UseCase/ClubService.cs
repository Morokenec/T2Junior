using MauiApp1.Models.ClubModels.Club;
using MauiApp1.Models.ClubModels.ClubList;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase.Interface;
using System.Diagnostics;

namespace MauiApp1.Services.UseCase
{
    /// <summary>
    /// Сервис для работы с клубами.
    /// </summary>
    /// <remarks>
    /// Этот сервис предоставляет методы для получения списка клубов и информации о конкретном клубе.
    /// </remarks>
    public class ClubService : IClubService
    {
        private readonly HttpClient _httpClient;
        private readonly IJsonDeserializerService _jsonDeserializerService;

        /// <summary>
        /// Конструктор сервиса ClubService.
        /// </summary>
        /// <param name="httpClient">HTTP-клиент для выполнения запросов.</param>
        /// <param name="jsonDeserializerService">Сервис для десериализации JSON.</param>
        public ClubService(HttpClient httpClient, IJsonDeserializerService jsonDeserializerService)
        {
            _httpClient = httpClient;
            _jsonDeserializerService = jsonDeserializerService;
        }

        /// <summary>
        /// Получает список клубов для текущего пользователя.
        /// </summary>
        /// <returns>Список клубов или null в случае ошибки.</returns>
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

        /// <summary>
        /// Получает информацию о клубе по его идентификатору.
        /// </summary>
        /// <param name="clubId">Идентификатор клуба.</param>
        /// <returns>Информация о клубе или null в случае ошибки.</returns>
        public async Task<Club> GetClubById(string clubId)
        {
            try
            {
                string url = $"{AppSetings.base_url}/api/Clubs/{clubId}/profile";
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
    }
}
