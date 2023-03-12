using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Server.Helpers;
using Server.Models;
using static Server.Helpers.ExceptionHandler;

namespace Server.Controllers
{
    //// POST Requests
    public partial class CarController : Controller
    {
        // Adds a car with specific values
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCar([FromBody] Car car)
        {
            try
            {
                // Validate the car object
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Request: " + ModelState);
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
                return Created("Car was created successfully!", car);
            }
            catch (Exception exception)
            {
                return StatusCode(500, "Failed to add a car, unknown error" + exception);
            }

        }

        // Uploading Car Image
        [HttpPost]
        [Authorize]
        [Route("Image")]
        public async Task<IActionResult> UploadCarImage()
        {
            try
            {
                var boundary = Request.GetMultipartBoundary();
                if (string.IsNullOrEmpty(boundary))
                {
                    return BadRequest("Missing content-type boundary.");
                }

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
                return Created("Car image uploaded successfully!", new { file });

            }
            catch (Exception exception)
            {
                return StatusCode(500, "Failed to upload a car image, unknown error:"+ exception);
            }

        }



    }
}

