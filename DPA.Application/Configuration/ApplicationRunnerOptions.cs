namespace DPA.Application.Configuration
{
    public class ApplicationRunnerOptions
    {
        public string? Source { get; set; }
        public int? DistrictId { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public int? Limit { get; set; }
        public string? Group { get; set; }
        public string? OutputFileName { get; set; }
    }
}
