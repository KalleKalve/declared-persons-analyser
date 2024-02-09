namespace DPA.Domain.Repositories
{
    public class DeclaredPersonsQueryParameters
    {
        public int? DistrictId { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public int Limit { get; set; }
        public string? Group { get; set; }
    }
}
