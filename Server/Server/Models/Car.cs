using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class Car
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Car name is required.")]
        [MaxLength(50, ErrorMessage = "Car name cannot exceed 50 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Car category is required.")]
        [RegularExpression("^(Family|Mini|Truck|Luxury|Sports|SUV)$", ErrorMessage = "Invalid Car category.")]
        public string? Category { get; set; } // 1.Family 2.Mini 3.Truck 4.Luxury 5.Sports 6.SUV

        [Range(1, 9999999, ErrorMessage = "Car price must be between 1 and 9,999,999.")]
        public double Price { get; set; }

        [Range(0, 1000, ErrorMessage = "Units in stock must be between 1 and 1,000.")]
        public int UnitsInStock { get; set; }

        [Range(1886, 2023, ErrorMessage = "Model year must be between 1886 and 2023.")]
        public int? ModelYear { get; set; }

        public string? ImageSrc { get; set; }

    }
}
