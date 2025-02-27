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
    public class NoteService : INoteService
    {
        private readonly HttpClient _httpClient;
        private readonly IJsonDeserializerService _jsonDeserializerService;

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

        public async Task<Note> GetNoteByIdAsync(string noteId)
        {
            try
            {
                // Пример URL для получения конкретной заметки по её ID
                string url = $"{AppSettings.base_url}/api/Notes/{noteId}";

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
