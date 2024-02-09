namespace DPA.Infrastructure.Data.ODataClients
{
    public class DeclaredPersonsODataQueryParameters
    {
        public long? Top { get; set; }
        public int? Skip { get; set; }
        public string? Filter { get; set; }
        public string? Select { get; set; }
        public string? Orderby { get; set; }
        public bool OrderByDesc { get; set; }
    }
}
