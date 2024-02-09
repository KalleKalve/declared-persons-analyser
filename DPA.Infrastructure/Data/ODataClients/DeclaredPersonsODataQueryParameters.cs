namespace DPA.Infrastructure.Data.ODataClients
{
    public class DeclaredPersonsODataQueryParameters
    {
        public int? Top { get; set; }
        public int? Skip { get; set; }
        public string? Filter { get; set; }
        public string? Select { get; set; }
        public Dictionary<string, bool>? Orderby { get; set; }
    }
}
