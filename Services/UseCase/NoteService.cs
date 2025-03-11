using MauiApp1.DataModel;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase.Interface;
using Microsoft.Maui.ApplicationModel.Communication;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services.UseCase
{
    /// <summary>
    /// Сервис для работы с заметками.
    /// </summary>
    /// <remarks>
    /// Предоставление методов для получения списка заметок и информации о конкретной заметке.
    /// </remarks>
    public class NoteService : INoteService
    {
        private readonly HttpClient _httpClient;
        private readonly IJsonDeserializerService _jsonDeserializerService;

        /// <summary>
        /// Конструктор сервиса NoteService.
        /// </summary>
        /// <param name="httpClient">HTTP-клиент для выполнения запросов.</param>
        /// <param name="jsonDeserializerService">Сервис для десериализации JSON.</param>
        public NoteService(HttpClient httpClient, IJsonDeserializerService jsonDeserializerService)
        {
            _httpClient = httpClient;
            _jsonDeserializerService = jsonDeserializerService;
        }

        public async Task<List<Note>> GetNotesAsync(Guid idOwner)
        {
            try
            {
                string url = $"{AppSettings.base_url}/api/Notes/get-by-id-owner/{idOwner.ToString()}";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                string responseContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"[DEBUG] Response: {responseContent}");

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"[ERROR] HTTP {response.StatusCode}: {responseContent}");
                    return null;
                }

                // Десериализация ответа в список заметок
                return _jsonDeserializerService.Deserialize<List<Note>>(responseContent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Получение информации о заметке по её идентификатору.
        /// </summary>
        /// <param name="noteId">Идентификатор заметки.</param>
        /// <returns>Информация о заметке или null в случае ошибки.</returns>
        public async Task<Note> GetNoteByIdAsync(string noteId)
        {
            try
            {
                string url = $"{AppSettings.base_url}/api/Notes/{noteId}";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                string responseContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"[DEBUG] Response: {responseContent}");

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"[ERROR] HTTP {response.StatusCode}: {responseContent}");
                    return null;
                }

                return _jsonDeserializerService.Deserialize<Note>(responseContent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Note>> GetNewsAsync()
        {
            try
            {
                string url = $"{AppSettings.base_url}/api/NewsFeed/{AppSettings.test_user_guid}";
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                string responseContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"[DEBUG] Response: {responseContent}");

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"[ERROR] HTTP {response.StatusCode}: {responseContent}");
                    return null;
                }

                return _jsonDeserializerService.Deserialize<List<Note>>(responseContent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<string> SendNoteAsync(Guid idOwner, string name, string description)
        {
            string url = $"{AppSettings.base_url}/api/Notes/create/{idOwner.ToString()}";
            var noteDto = new { name = name, description = description};

            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, noteDto);
                if (response.IsSuccessStatusCode)
                {
                    var authResponse = await response.Content.ReadFromJsonAsync<Note>();
                    return authResponse?.Id;
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

        public async Task SendNoteAsync(Guid idOwner, string name, string description, Stream mediaFile)
        {
            try
            {
                string idNote = await SendNoteAsync(idOwner, name, description);

                if (string.IsNullOrEmpty(idNote))
                {
                    Debug.WriteLine("Ошибка: не удалось получить ID заметки");
                    return;
                }
                string url = $"{AppSettings.base_url}/api/MediaNotes/add-media-to-note/{idNote}";

                using var content = new MultipartFormDataContent();

                if (mediaFile.CanSeek)
                {
                    mediaFile.Position = 0;
                }

                var fileContent = new StreamContent(mediaFile);
                content.Add(fileContent, "File", "uploaded_file.png");

                content.Add(new StringContent(AppSettings.test_user_guid), "IdUser");

                using var response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка загрузки: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Внутренняя ошибка: {ex.InnerException.Message}");
                }
            }
        }
    }
}