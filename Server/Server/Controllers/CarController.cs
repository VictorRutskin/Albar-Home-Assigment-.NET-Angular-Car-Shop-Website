using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Server.Data;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : Controller
    {
            private readonly IWebHostEnvironment _hostingEnvironment;

            private readonly MyDbContext mydbcontext;

            private readonly IWebHostEnvironment _environment;


        public CarController(MyDbContext mydbcontext, IWebHostEnvironment environment)
            {
                this.mydbcontext = mydbcontext;
                this._environment = environment;


        }

            //// POST
            
            // Adds a car with specific values
            [HttpPost]
            public async Task<IActionResult> AddCar([FromBody] Car car)
            {

                await mydbcontext.AddAsync(car);
                await mydbcontext.SaveChangesAsync();

                return Ok(car);
            }

            // Uploading Car Image
            [HttpPost]
            [Route("Image")]
            public async Task<IActionResult> UploadCarImage()
            {
                try
                {
                    var formCollection = await Request.ReadFormAsync();
                    var file = formCollection.Files.First();

                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
                        var fullPath = Path.Combine(Paths.GetGlobalPath(), fileName.ToString());
                        var dbPath = Path.Combine(Paths.GetLocalPath(), fileName.ToString());
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        return Ok(new { dbPath });
                    }
                    else
                    {
                        return BadRequest();
                    }

                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex}");
                }
            }


            



            //// GET 

            // Returns all cars
            [HttpGet]
            public async Task<IActionResult> GetAllCars()
            {
                var cars = await mydbcontext.Cars.ToListAsync();

                foreach (var car in cars)
                {
                    car.ImageSrc = System.IO.File.ReadAllBytesAsync(Paths.GetLocalPath() +@"\"+ car.ImageSrc!).ToString();
                }

                return Ok(cars);
            }

            // Returns a specific car
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
                    car.ImageSrc = System.IO.File.ReadAllBytesAsync(Paths.GetLocalPath() + @"\" + car.ImageSrc!).ToString();
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



            //// PUT 

            // Updates car values
            [HttpPut]
            [Route("{id:}")]
            public async Task<IActionResult> UpdateCar([FromRoute] long id, Car UpdatedCar)
            {
                var car = await mydbcontext.Cars.FirstOrDefaultAsync(x => x.Id == id);

                if (car == null)
                {
                    return NotFound();
                }
                car.Name = UpdatedCar.Name;
                car.Category = UpdatedCar.Category;
                car.Price = UpdatedCar.Price;
                car.UnitsInStock = UpdatedCar.UnitsInStock;
                car.ModelYear = UpdatedCar.ModelYear;

                await mydbcontext.SaveChangesAsync();

                return Ok(car);
            }



            //// DELETE 
            
            // Deletes a car using id
            [HttpDelete]
            [Route("{id:}")]
            public async Task<IActionResult> DeleteCar([FromRoute] long id)
            {
                var car = await mydbcontext.Cars.FirstOrDefaultAsync(x => x.Id == id);

                if (car == null)
                {
                    return NotFound();
                }
                mydbcontext.Cars.Remove(car);
                await mydbcontext.SaveChangesAsync();

                return Ok(car);
            }


        }
    }

