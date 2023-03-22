using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Helpers;
using Server.Models;
using System;
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
                        InvalidImageCheck = true;
                    }
                }
                if (InvalidImageCheck)
                {
                    // Partial Content because not all images loaded
                    return StatusCode(StatusCodes.Status206PartialContent, cars);
                }

                return Ok(cars);
            }
            catch (Exception exception)
            {
                string myError = "Failed to get all cars, unknown error:";
                MyLogger.LogException(myError, exception);
                return StatusCode(StatusCodes.Status500InternalServerError, myError);
            }

        }

        // Returns a specific car using id
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCar([FromRoute] long id)
        {
            try
            {
                var car = await mydbcontext.Cars.FirstOrDefaultAsync(x => x.Id == id);

                if (car == null)
                {
                    throw new NotFoundInDbException();
                }

                bool InvalidImageCheck = false;

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
                    InvalidImageCheck = true;
                }
                if (InvalidImageCheck)
                {
                    // Partial Content because not all images loaded
                    return StatusCode(StatusCodes.Status206PartialContent, car);
                }

                return Ok(car);
            }
            catch (NotFoundInDbException notFoundInDbException)
            {
                string myError = "Car with id: " + id.ToString() + " not found in the database";
                MyLogger.LogException(myError, notFoundInDbException);
                return NotFound(myError);
            }
            catch (Exception exception)
            {
                string myError = "Failed to get a car with id, unknown error:";
                MyLogger.LogException(myError, exception);
                return StatusCode(StatusCodes.Status500InternalServerError, myError);
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

                if (car == null)
                {
                    throw new NotFoundInDbException();
                }

                return Ok(car);
            }
            catch (NotFoundInDbException notFoundInDbException)
            {
                string myError = "Car with parameters: " + myCar.ToString() + " not found in the database";
                MyLogger.LogException(myError, notFoundInDbException);
                return NotFound(myError);
            }
            catch (Exception exception)
            {
                string myError = "Failed to get a car with name, unknown error:";
                MyLogger.LogException(myError, exception);
                return StatusCode(StatusCodes.Status500InternalServerError, myError);
            }
        }

        // Returns a car id if a car with the name exists
        [HttpGet]
        [Route("GetCarWithName")]
        public async Task<IActionResult> GetCarWithName([FromQuery] Car myCar)
        {
            try
            {
                var car = await mydbcontext.Cars.FirstOrDefaultAsync(x => x.Name == myCar.Name);

                if (car == null)
                {
                    throw new NotFoundInDbException();
                }

                return Ok(car.Id);
            }
            catch (NotFoundInDbException notFoundInDbException)
            {
                string myError = "Car with parameters: " + myCar.ToString() + " not found in the database";
                MyLogger.LogException(myError, notFoundInDbException);
                return NotFound(myError);
            }
            catch (Exception exception)
            {
                string myError = "Failed to get a car with name, unknown error:";
                MyLogger.LogException(myError, exception);
                return StatusCode(StatusCodes.Status500InternalServerError, myError);
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

                if (cars == null || !cars.Any())
                {
                    throw new NotFoundInDbException();
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
            catch (NotFoundInDbException notFoundInDbException)
            {
                string myError = "Failed go get 3 cars: ";
                MyLogger.LogException(myError, notFoundInDbException);
                return NotFound(myError + notFoundInDbException.Message);
            }
            catch (Exception exception)
            {
                string myError = "Failed to get top 3 cars, unknown error:";
                MyLogger.LogException(myError, exception);
                return NotFound(myError + exception.Message);
            }
        }

        // Returns the car image
        [HttpGet("image/{id}")]
        public async Task<IActionResult> GetCarImage(long id)
        {
            try
            {
                var car = await mydbcontext.Cars.FindAsync(id);

                if (car == null)
                {
                    throw new NotFoundInDbException();
                }

                var imagePath = Path.Combine(Paths.GetGlobalPath(), car.ImageSrc!);

                if (!System.IO.File.Exists(imagePath))
                {
                    return NotFound();
                }

                var imageBytes = await System.IO.File.ReadAllBytesAsync(imagePath);

                return File(imageBytes, "image/jpeg");
            }
            catch (NotFoundInDbException notFoundInDbException)
            {
                string myError = "Car with id: " + id.ToString() + " was not found in the database.";
                MyLogger.LogException(myError, notFoundInDbException);
                return NotFound(myError);
            }
            catch (Exception exception)
            {
                string myError = "Failed to get car image with id " + id.ToString() + ", unknown error:";
                MyLogger.LogException(myError, exception);
                return StatusCode(StatusCodes.Status500InternalServerError, myError + exception.Message);
            }
        }



    }
}

