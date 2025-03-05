using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using T2JuniorMobile.Model.Profile;
using T2JuniorAPI.DTOs;

namespace T2JuniorMobile.Services
{
    /// <summary>
    /// Сервис для управления профилем пользователя.
    /// </summary>
    public class ProfileServices
    {
        private readonly HttpClient _httpClient;
        private readonly string ProfileEndpoint = "http://localhost:5138/api/Account/profile/{id}";

        /// <summary>
        /// Конструктор класса ProfileServices.
        /// </summary>
        /// <param name="httpClient">HTTP-клиент для выполнения запросов.</param>
        public ProfileServices(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        /// Получение идентификатора пользователя из безопасного хранилища.
        /// </summary>
        /// <returns>Идентификатор пользователя.</returns>
        private async Task<string> GetUserIdAsync()
        {
            try
            {
                var userId = await SecureStorage.Default.GetAsync("user_uid");
                return userId ?? throw new InvalidOperationException("User ID не найден");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении User ID: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Получение профиля пользователя.
        /// </summary>
        /// <returns>Профиль пользователя.</returns>
        public async Task<UserProfileDTO> GetProfileAsync()
        {
            try
            {
                var userId = await GetUserIdAsync();

                var requestUri = ProfileEndpoint.Replace("{id}", await SecureStorage.GetAsync("user_uid"));

                var response = await _httpClient.GetAsync(requestUri);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Ошибка при получении профиля: {response.StatusCode}, {error}");
                    return null;
                }

                if (response.Content == null)
                {
                    Console.WriteLine("Содержимое ответа отсутствует");
                    return null;
                }

                var profile = await response.Content.ReadFromJsonAsync<UserProfileDTO>();
                return profile ?? throw new InvalidOperationException("Не удалось десериализовать профиль пользователя");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении профиля: {ex.Message}");
                throw;
            }
        }
    }
}
