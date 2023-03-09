namespace Server.Models
{
    public class Car
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public double Price { get; set; }
        public int UnitsInStock { get; set; }
        public int? ModelYear { get; set; }
    }
}
