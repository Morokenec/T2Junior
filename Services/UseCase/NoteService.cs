using MauiApp1.DataModel;
using MauiApp1.Services.AppHelper;
using MauiApp1.Services.UseCase.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        /// <summary>
        /// Получение списка заметок текущего пользователя.
        /// </summary>
        /// <returns>Список заметок или null в случае ошибки.</returns>
        public async Task<List<Note>> GetNotesAsync()
        {
            try
            {
                string url = $"{AppSetings.base_url}/api/Notes/get-by-id-owner/{AppSetings.test_user_guid}";

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
                // Пример URL для получения конкретной заметки по её ID
                string url = $"{AppSetings.base_url}/api/Notes/{noteId}";

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                string responseContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"[DEBUG] Response: {responseContent}");

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"[ERROR] HTTP {response.StatusCode}: {responseContent}");
                    return null;
                }

                // Десериализация ответа в объект Note
                return _jsonDeserializerService.Deserialize<Note>(responseContent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception: {ex.Message}");
                return null;
            }
        }
    }
}
