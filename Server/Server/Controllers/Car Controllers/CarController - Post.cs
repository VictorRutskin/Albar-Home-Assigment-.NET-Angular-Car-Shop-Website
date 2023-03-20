using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Server.Helpers;
using Server.Models;
using System;
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
                    throw new ModelStateException();
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
            catch (ModelStateException modelStateException)
            {
                string myError = "Invalid modelstate: ";
                MyLogger.LogException(myError, modelStateException);
                return BadRequest(myError + modelStateException.Message);
            }
            catch (Exception exception)
            {
                string myError = "Failed to add a car, unknown error:";
                MyLogger.LogException(myError, exception);
                return BadRequest(myError + exception.Message);
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
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.FirstOrDefault();

                if (file == null || file.Length == 0)
                {
                    throw new ImageAddingException("Failed to upload a car image, file was empty.");
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
            catch (ImageAddingException ex)
            {
                MyLogger.LogException(ex.Message, ex);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                string message = "Failed to upload a car image, unknown error.";
                MyLogger.LogException(message, ex);
                return NotFound(message);
            }
        }



    }
}

