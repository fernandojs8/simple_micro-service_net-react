namespace Domain_simple_microservice.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public double Price { get; set; }
        public string Category { get; set; } = "";
        public DateTime Date { get; set; }
    }
}
