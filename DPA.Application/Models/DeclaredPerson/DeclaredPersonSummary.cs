namespace DPA.Application.Models.DeclaredPerson
{
    public class DeclaredPersonSummary
    {
        public int Max { get; set; }
        public int Min { get; set; }
        public int Average { get; set; }

        public DeclaredPersonMaxDrop MaxDrop { get; set; }
        public DeclaredPersonMaxIncrease MaxIncrease { get; set; }
    }
}
