using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApp1.Services.AppHelper
{ 
  /// <summary>
  /// Сервис для десериализации JSON-строк в объекты.
  /// </summary>
    public class JsonDeserializerService : IJsonDeserializerService
    {
        /// <summary>
        /// Десериализует JSON-строку в объект указанного типа.
        /// </summary>
        /// <typeparam name="T">Тип объекта, в который будет десериализована JSON-строка.</typeparam>
        /// <param name="json">JSON-строка для десериализации.</param>
        public T Deserialize<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("JSON-строка не должна быть пустой", nameof(json));

            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
