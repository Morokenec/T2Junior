using MauiApp1.Models.Profile;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase.Interface;
using System.Diagnostics;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using MauiApp1.Models;
using MauiApp1.Models.ProfileModels;

namespace MauiApp1.Services.UseCase
{
    /// <summary>
    /// Сервис для работы с профилем пользователя.
    /// </summary>
    public class ProfileService : IProfileService
    {
        private readonly HttpClient _httpClient;
        private readonly IJsonDeserializerService _jsonDeserializerService;

        /// <summary>
        /// Конструктор сервиса профиля.
        /// </summary>
        /// <param name="httpClient">HTTP-клиент.</param>
        /// <param name="jsonDeserializerService">Сервис десериализации JSON.</param>
        public ProfileService(HttpClient httpClient, IJsonDeserializerService jsonDeserializerService)
        {
            _httpClient = httpClient;
            _jsonDeserializerService = jsonDeserializerService;
        }

        /// <summary>
        /// Получает данные профиля пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Данные профиля пользователя.</returns>
        public async Task<ProfileResponse> GetProfileDataAsync(Guid userId)
        {
            try
            {
                string url = $"{AppSettings.base_url}/api/Account/profile/{userId.ToString()}";

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

        /// <summary>
        /// Загружает аватар пользователя на сервер.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="chosenImage">Поток изображения.</param>
        public async Task SetAvatarProfileUploadServer(Guid userId, Stream chosenImage)
        {
            try
            {
                string url = $"{AppSettings.base_url}/api/Media/set-avatar-for-user";
                using var content = new MultipartFormDataContent();

                content.Add(new StreamContent(chosenImage), fileName: "1.png", name: "File");
                content.Add(new StringContent(userId.ToString()), name: "IdUser");

                using var response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception: {ex.Message}");
            }
        }

        /// <summary>
        /// Выполняет вход пользователя.
        /// </summary>
        /// <param name="email">Email пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Токен аутентификации или null в случае ошибки.</returns>
        public async Task<string?> LoginAsync(string email, string password)
        {
            string url = $"{AppSettings.base_url}/api/Account/login";
            var authRequest = new AuthRequest { Email = email, Password = password };

            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, authRequest);
                if (response.IsSuccessStatusCode)
                {
                    var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
                    return authResponse?.Token;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Ошибка авторизации: {response.StatusCode}, {error}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Некорректное значение: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Получает список подписок пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Список подписок пользователя.</returns>
        public async Task<List<UserSocial>> GetUserSubscriptions(Guid userId)
        {
            try
            {
                string url = $"{AppSettings.base_url}/api/UserSubscribers/subscriptions?userId={userId.ToString()}";
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                string responseContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"[DEBUG] Response: {responseContent}");

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"[ERROR] HTTP {response.StatusCode}: {responseContent}");
                    return null;
                }

                return _jsonDeserializerService.Deserialize<List<UserSocial>>(responseContent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Получает список подписчиков пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Список подписчиков пользователя.</returns>
        public async Task<List<UserSocial>> GetUserSubscribers(Guid userId)
        {
            try
            {
                string url = $"{AppSettings.base_url}/api/UserSubscribers/subscribers?userId={userId.ToString()}";
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                string responseContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"[DEBUG] Response: {responseContent}");

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"[ERROR] HTTP {response.StatusCode}: {responseContent}");
                    return null;
                }

                return _jsonDeserializerService.Deserialize<List<UserSocial>>(responseContent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception: {ex.Message}");
                return null;
            }
        }
    }
}
