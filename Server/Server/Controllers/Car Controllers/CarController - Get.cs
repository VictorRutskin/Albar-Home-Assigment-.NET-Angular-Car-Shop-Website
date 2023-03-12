using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Helpers;
using Server.Models;

namespace Server.Controllers
{
    //// GET Requests
    public partial class CarController : Controller
    {
        // Returns all cars
        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            bool InvalidImageCheck = false;

            var cars = await mydbcontext.Cars.ToListAsync();

            foreach (var car in cars)
            {
                // If image exists use it, else use empty
                string imagePath = Paths.GetLocalPath() + @"\" + car.ImageSrc!;
                if (System.IO.File.Exists(imagePath))
                {
                    car.ImageSrc = System.IO.File.ReadAllBytesAsync(Paths.GetLocalPath() + @"\" + car.ImageSrc!).ToString();
                }
                //no image src detected
                else
                {
                    car.ImageSrc = "";
                }
            }
            if (InvalidImageCheck)
            {
                // Partial Content because not all images loaded
                return StatusCode(StatusCodes.Status206PartialContent);
            }

            return Ok(cars);
        }

        // Returns a specific car using id
        [HttpGet]
        [Route("{id:}")]
        public async Task<IActionResult> GetCar([FromRoute] long id)
        {
            var car = await mydbcontext.Cars.FirstOrDefaultAsync(x => x.Id == id);

            if (car == null)
            {
                return NotFound();
            }


            car.ImageSrc = System.IO.File.ReadAllBytesAsync(Paths.GetLocalPath() + @"\" + car.ImageSrc!).ToString();


            return Ok(car);
        }

        // Returns a specific car using query, needed only for new car.
        [HttpGet]
        [Route("GetCar2")]

        public async Task<IActionResult> GetCar2([FromQuery] Car myCar)
        {
            var car = await mydbcontext.Cars.FirstOrDefaultAsync(x => x.Name == myCar.Name && x.Category == myCar.Category && x.Price == myCar.Price);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        //  Returns a car id if a car with the name exists
        [HttpGet]
        [Route("GetCarWithName")]
        public async Task<IActionResult> GetCarWithName([FromQuery] Car myCar)
        {
            var car = await mydbcontext.Cars.FirstOrDefaultAsync(x => x.Name == myCar.Name);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car.Id);
        }

        // Returns top 3 cars with most UnitsInStock value
        [HttpGet]
        [Route("Top3")]
        public async Task<IActionResult> Get3CarExtras()
        {
            var cars = await mydbcontext.Cars.OrderByDescending(c => c.UnitsInStock)
                                             .Take(3)
                                             .ToListAsync();

            if (cars == null || !cars.Any())
            {
                return NotFound();
            }

            foreach (var car in cars)
            {

                // If image exists use it, else use empty
                string imagePath = Paths.GetLocalPath() + @"\" + car.ImageSrc!;
                if (System.IO.File.Exists(imagePath))
                {
                    car.ImageSrc = System.IO.File.ReadAllBytesAsync(Paths.GetLocalPath() + @"\" + car.ImageSrc!).ToString();
                }
                else
                {
                    car.ImageSrc = "";
                }
            }

            return Ok(cars);
        }

        // Returns the car image
        [HttpGet("image/{id}")]
        public async Task<IActionResult> GetCarImage(long id)
        {
            var car = await mydbcontext.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var imagePath = Path.Combine(Paths.GetGlobalPath(), car.ImageSrc!);

            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound();
            }

            var imageBytes = System.IO.File.ReadAllBytes(imagePath);


            return File(imageBytes, "image/jpeg");
        }


    }
}

