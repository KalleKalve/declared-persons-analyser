namespace DPA.Application.Models.DeclaredPerson
{
    public class DeclaredPersonOutput
    {
        public List<DeclaredPersonData> Data { get; set; } = new List<DeclaredPersonData>();
        public DeclaredPersonSummary Summary { get; set; }
    }
}
