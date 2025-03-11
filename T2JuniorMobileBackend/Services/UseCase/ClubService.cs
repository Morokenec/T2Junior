using MauiApp1.Models.ClubModels.Club;
using MauiApp1.Models.ClubModels.ClubList;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase.Interface;
using System.Diagnostics;
using System.Text.Json;
using System.Text;

namespace MauiApp1.Services.UseCase
{
    /// <summary>
    /// Сервис для работы с клубами, включает получение информации о клубах, подписку и загрузку аватаров.
    /// </summary>
    public class ClubService : IClubService
    {
        private readonly HttpClient _httpClient;
        private readonly IJsonDeserializerService _jsonDeserializerService;

        /// <summary>
        /// Конструктор сервиса клубов.
        /// </summary>
        /// <param name="httpClient">Экземпляр HttpClient для выполнения HTTP-запросов.</param>
        /// <param name="jsonDeserializerService">Сервис для десериализации JSON-ответов.</param>
        public ClubService(HttpClient httpClient, IJsonDeserializerService jsonDeserializerService)
        {
            _httpClient = httpClient;
            _jsonDeserializerService = jsonDeserializerService;
        }

        /// <summary>
        /// Получает список клубов пользователя.
        /// </summary>
        /// <returns>Список объектов ClubList или null в случае ошибки.</returns>
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

                return _jsonDeserializerService.Deserialize<List<ClubList>>(responseContent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Получает информацию о конкретном клубе по его идентификатору.
        /// </summary>
        /// <param name="clubId">Идентификатор клуба.</param>
        /// <returns>Объект Club или null в случае ошибки.</returns>
        public async Task<Club> GetClubById(Guid clubId)
        {
            try
            {
                string url = $"{AppSettings.base_url}/api/Clubs/{clubId}/profile?userId={AppSettings.test_user_guid}";
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                string responseContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"[DEBUG] Response: {responseContent}");

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"[ERROR] HTTP {response.StatusCode}: {responseContent}");
                    return null;
                }

                return _jsonDeserializerService.Deserialize<Club>(responseContent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Подписывает пользователя на клуб с указанной ролью.
        /// </summary>
        /// <param name="clubId">Идентификатор клуба.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="roleId">Идентификатор роли пользователя.</param>
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

        /// <summary>
        /// Загружает и устанавливает аватар для клуба.
        /// </summary>
        /// <param name="clubId">Идентификатор клуба.</param>
        /// <param name="userId">Идентификатор пользователя, выполняющего действие.</param>
        /// <param name="chosenImage">Поток изображения для загрузки.</param>
        public async Task SetAvatarClubUploadServer(Guid clubId, Guid userId, Stream chosenImage)
        {
            try
            {
                string url = $"{AppSettings.base_url}/api/MediaClubs/set-avatar/{clubId}";

                using var content = new MultipartFormDataContent();

                content.Add(
                    new StreamContent(chosenImage),
                    fileName: "1.png",
                    name: "File");

                content.Add(
                    new StringContent(userId.ToString()),
                    name: "IdUser");

                using var response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception: {ex.Message}");
            }
        }
    }
}
