using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using T2JuniorMobile.Model.Auth;

namespace T2JuniorMobile.Services
{
    /// <summary>
    /// Сервис для управления аутентификацией пользователей.
    /// </summary>
    public class AccountService
    {
        private readonly HttpClient _httpClient;
        private const string AuthEndpoint = "http://localhost:5138/api/Account/login";

        /// <summary>
        /// Конструктор класса AccountService.
        /// </summary>
        /// <param name="httpClient">HTTP-клиент для выполнения запросов.</param>
        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        /// <summary>
        /// Выполнение входа пользователя в систему.
        /// </summary>
        /// <param name="email">Электронная почта пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        public async Task<string?> LoginAsync(string email, string password)
        {
            var authRequest = new AuthRequest { Email = email, Password = password };

            try
            {
                var response = await _httpClient.PostAsJsonAsync(AuthEndpoint, authRequest);

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
                Console.WriteLine($"Не коректное значение: {ex.Message}");
            }

            return null;
        }

    }
}
