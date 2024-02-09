namespace DPA.Application.Interfaces
{
    public interface IJsonExportService
    {
        Task ExportToJsonFileAsync<T>(T data, string filePath);
    }
}
