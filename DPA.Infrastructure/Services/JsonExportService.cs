using DPA.Application.Interfaces;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DPA.Infrastructure.Services
{
    public class JsonExportService : IJsonExportService
    {
        public async Task ExportToJsonFileAsync<T>(T data, string filePath)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            using (var stream = File.Create(filePath))
            {
                await JsonSerializer.SerializeAsync(stream, data, options);
            }
        }
    }
}