namespace DPA.Domain.Repositories
{
    public class DeclaredPersonsQueryParameters
    {
        public int Top { get; set; } = 10;
        public int Skip { get; set; } = 0;
        public string Filter { get; set; }
        public string Select { get; set; }
        public string Orderby { get; set; }
    }
}
