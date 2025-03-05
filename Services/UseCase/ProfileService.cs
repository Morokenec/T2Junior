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
    public class ProfileService : IProfileService
    {
        private readonly HttpClient _httpClient;
        private readonly IJsonDeserializerService _jsonDeserializerService;

        public ProfileService(HttpClient httpClient, IJsonDeserializerService jsonDeserializerService)
        {
            _httpClient = httpClient;
            _jsonDeserializerService = jsonDeserializerService;
        }

        public async Task<ProfileResponse> GetProfileDataAsync()
        {
            try
            {
                string url = $"{AppSettings.base_url}/api/Account/profile/{AppSettings.test_user_guid}";

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

        public async Task SetAvatarProfileUploadServer(Guid userId, Stream chosenImage)
        {
            try
            {
                string url = $"{AppSettings.base_url}/api/Media/set-avatar-for-user";

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
                Console.WriteLine($"Не коректное значение: {ex.Message}");
            }

            return null;
        }

        public async Task<List<UserSocial>> GetUserSubscribers(Guid userId)
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
    }
}
