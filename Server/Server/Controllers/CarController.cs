using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Server.Data;
using Server.Models;
using Server.Services;
using System.Web;
using static Server.Services.ExceptionHandler;

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
        [Authorize]
        public async Task<IActionResult> AddCar([FromBody] Car car)
        {
            // Validate the car object
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // If car with the name exist do not add.
            var CheckingExistingNameCar = await mydbcontext.Cars.FirstOrDefaultAsync(x => x.Name == car.Name);

            if (CheckingExistingNameCar != null)
            {
                return Conflict("A car with this name already exists, duplicates are not allowed.");
            }

            await mydbcontext.AddAsync(car);
            await mydbcontext.SaveChangesAsync();

            // Setting the right imageSrc
            car.ImageSrc = "Car-" + car.Id + ".jpg";

            await mydbcontext.SaveChangesAsync();
            return Ok(car);
        }

        // Uploading Car Image
        [HttpPost]
        [Authorize]
        [Route("Image")]
        public async Task<IActionResult> UploadCarImage()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();

                if (file.Length == 0)
                {
                    return BadRequest("File is empty.");
                }


                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
                var fullPath = Path.Combine(Paths.GetGlobalPath(), fileName.ToString());
                var dbPath = Path.Combine(Paths.GetLocalPath(), fileName.ToString());
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return Ok(new { dbPath });

            }
            catch (InvalidFileException invalidFileException)
            {
                return BadRequest(invalidFileException.Message);
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
                try
                {
                    car.ImageSrc = System.IO.File.ReadAllBytesAsync(Paths.GetLocalPath() + @"\" + car.ImageSrc!).ToString();
                }
                //no image src detected
                catch { }
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

        //  Returns a specific car using query, needed only for new car.
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



        //// PUT 

        // Updates car values
        [HttpPut]
        [Route("{id:}")]
        public async Task<IActionResult> UpdateCar([FromRoute] long id, Car UpdatedCar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
        [Authorize]

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

            // If image exists delete, else dont
            string imagePath = Paths.GetLocalPath() + @"\" + car.ImageSrc!;
            if (System.IO.File.Exists(imagePath))
            {
                car.ImageSrc = System.IO.File.ReadAllBytesAsync(Paths.GetLocalPath() + @"\" + car.ImageSrc!).ToString();
            }

            return Ok(car);
        }


    }
}

