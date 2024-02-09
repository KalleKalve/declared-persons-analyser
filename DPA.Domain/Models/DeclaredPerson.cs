namespace DPA.Domain.Models
{
    public class DeclaredPerson
    {
        public int id { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public string value { get; set; }
        public int district_id { get; set; }
        public string district_name { get; set; }
    }
}
