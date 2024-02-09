namespace DPA.Domain.Models
{
    public class DeclaredPersons
    {
        public int id { get; set; }
        public int? year { get; set; }
        public int? month { get; set; }
        public int? day { get; set; }
        public int value { get; set; }
        public int district_id { get; set; }
        public string district_name { get; set; }
    }
}
