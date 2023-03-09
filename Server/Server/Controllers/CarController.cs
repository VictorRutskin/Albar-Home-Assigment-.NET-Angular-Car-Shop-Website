using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Server.Data;
using Server.Models;
using System;

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

                    var folderName = Path.Combine("Resources", "Images");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
                        var fullPath = Path.Combine(pathToSave, fileName.ToString());
                        var dbPath = Path.Combine(folderName, fileName.ToString());
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
                    car.ImageSrc = Url.Action("GetCarImage", "Cars", new { id = car.Id });
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

                //var imageFilePath = Path.Combine("Images", "Cars", car.ImageSrc);

                //if (!System.IO.File.Exists(imageFilePath))
                //{
                //    return NotFound();
                //}

                //var imageFile = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);

                //return File(imageFile, "image/jpeg");

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

                return Ok(cars);
            }

            // Returns the car image
            [HttpGet("image/{id}")]
            public IActionResult GetCarImage(long id)
            {
                var car = mydbcontext.Cars.Find(id);

                if (car == null)
                {
                    return NotFound();
                }

                var imagePath = Path.Combine("Images", "Cars", car.ImageSrc);
                var imageFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, imagePath);

                if (!System.IO.File.Exists(imageFilePath))
                {
                    return NotFound();
                }

                var imageFile = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);

                return File(imageFile, "image/jpeg");
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

