using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApp1.Services.AppHelper
{
    public class JsonDeserializerService : IJsonDeserializerService
    {
        public T Deserialize<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("JSON-строка не должна быть пустой", nameof(json));

            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
