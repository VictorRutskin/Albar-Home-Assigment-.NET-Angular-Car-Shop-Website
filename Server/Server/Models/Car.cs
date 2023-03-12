using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    // My Car Model, id defined by the mssql server after sending new car to it
    public class Car
    {
        public long Id { get; set; }

        [MaxLength(50, ErrorMessage = "Car name cannot exceed 50 characters.")]
        public string? Name { get; set; }

        [RegularExpression("^(Family|Mini|Truck|Luxury|Sports|SUV)$", ErrorMessage = "Invalid Car category.")]
        public string? Category { get; set; } 

        [Range(0, 9999999, ErrorMessage = "Car price must be between 0 and 9,999,999.")]
        public double Price { get; set; }

        [Range(0, 1000, ErrorMessage = "Units in stock must be between 0 and 1,000.")]
        public int UnitsInStock { get; set; }

        [Range(1886, 2023, ErrorMessage = "Model year must be between 1886 and 2023.")]
        public int? ModelYear { get; set; }

        public string? ImageSrc { get; set; }


        public override string ToString()
        {
            return "Id: " + Id + ", " + "Name: " + Name + ", " + "Category: " + Category + ", " + "Price: " + Price + ", " + "UnitsInStock: " + UnitsInStock + ", "+"ModelYear: " + ModelYear + ", " +"ImageSrc: " + ImageSrc + ", ";
        }

    }
}
