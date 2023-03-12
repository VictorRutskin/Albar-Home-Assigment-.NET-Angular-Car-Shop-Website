using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Helpers;
using Server.Models;
using static Server.Helpers.ExceptionHandler;

namespace Server.Controllers
{
    //// GET Requests
    public partial class CarController : Controller
    {
        // Returns all cars
        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            try
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
            catch (Exception exception)
            {
                return StatusCode(500, "Failed to get all cars, unknown error" + exception);
            }

        }

        // Returns a specific car using id
        [HttpGet]
        [Route("{id:}")]
        public async Task<IActionResult> GetCar([FromRoute] long id)
        {
            try
            {
                var car = await mydbcontext.Cars.FirstOrDefaultAsync(x => x.Id == id);

                try
                {
                    if (car == null)
                    {
                        throw new NotFoundInDbException();
                    }
                }
                catch (NotFoundInDbException notFoundInDbException)
                {
                    return NotFound("Car with id: " + id.ToString() + ", " + notFoundInDbException.Message);
                }


                car.ImageSrc = System.IO.File.ReadAllBytesAsync(Paths.GetLocalPath() + @"\" + car.ImageSrc!).ToString();


                return Ok(car);
            }
            catch (Exception exception)
            {
                return StatusCode(500, "Failed to get a car with id, unknown error" + exception);
            }
        }

        // Returns a specific car using query, needed only for new car.
        [HttpGet]
        [Route("GetCar2")]

        public async Task<IActionResult> GetCar2([FromQuery] Car myCar)
        {
            try
            {
                var car = await mydbcontext.Cars.FirstOrDefaultAsync(x => x.Name == myCar.Name && x.Category == myCar.Category && x.Price == myCar.Price);

                try
                {
                    if (car == null)
                    {
                        throw new NotFoundInDbException();
                    }
                }
                catch (NotFoundInDbException notFoundInDbException)
                {
                    return NotFound("Car with parameters: " + myCar.ToString() + ", " + notFoundInDbException.Message);
                }

                return Ok(car);
            }
            catch (Exception exception)
            {
                return StatusCode(500, "Failed to get a car with name, unknown error" + exception);
            }
        }

        //  Returns a car id if a car with the name exists
        [HttpGet]
        [Route("GetCarWithName")]
        public async Task<IActionResult> GetCarWithName([FromQuery] Car myCar)
        {
            try
            {
                var car = await mydbcontext.Cars.FirstOrDefaultAsync(x => x.Name == myCar.Name);

                try
                {
                    if (car == null)
                    {
                        throw new NotFoundInDbException();
                    }
                }
                catch (NotFoundInDbException notFoundInDbException)
                {
                    return NotFound("Car with parameters: " + myCar.ToString() + ", " + notFoundInDbException.Message);
                }

                return Ok(car.Id);
            }
            catch (Exception exception)
            {
                return StatusCode(500, "Failed to get a car with name, unknown error" + exception);
            }
        }

        // Returns top 3 cars with most UnitsInStock value
        [HttpGet]
        [Route("Top3")]
        public async Task<IActionResult> Get3CarExtras()
        {
            try
            {
                var cars = await mydbcontext.Cars.OrderByDescending(c => c.UnitsInStock)
                                             .Take(3)
                                             .ToListAsync();
                try
                {
                    if (cars == null || !cars.Any())
                    {
                        throw new NotFoundInDbException();
                    }
                }
                catch (NotFoundInDbException notFoundInDbException)
                {
                    return NotFound("Failed go get 3 cars: " + notFoundInDbException.Message);
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
            catch (Exception exception)
            {
                return StatusCode(500, "Failed to get top 3 cars, unknown error" + exception);
            }
        }

        // Returns the car image
        [HttpGet("image/{id}")]
        public async Task<IActionResult> GetCarImage(long id)
        {
            try
            {
                var car = await mydbcontext.Cars.FindAsync(id);

                try
                {
                    if (car == null)
                    {
                        throw new NotFoundInDbException();
                    }
                }
                catch (NotFoundInDbException notFoundInDbException)
                {
                    return NotFound("Car with id: " + id.ToString() + ", " + notFoundInDbException.Message);
                }

                var imagePath = Path.Combine(Paths.GetGlobalPath(), car.ImageSrc!);

                if (!System.IO.File.Exists(imagePath))
                {
                    return NotFound();
                }

                var imageBytes = System.IO.File.ReadAllBytes(imagePath);


                return File(imageBytes, "image/jpeg");
            }
            catch (Exception exception)
            {
                return StatusCode(500, "Failed to get car image, unknown error" + exception);
            }

        }


    }
}

