namespace Server.Models
{
    public class Car
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; } // 1.Family 2.Mini 3.Truck 4.Luxury 5.Sports 6.SUV
        public double Price { get; set; }
        public int UnitsInStock { get; set; }
        public int? ModelYear { get; set; }
        public string? ImageSrc { get; set; }
    }
}
