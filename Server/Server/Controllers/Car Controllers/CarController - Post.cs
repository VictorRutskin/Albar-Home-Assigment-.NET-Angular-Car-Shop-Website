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
                try
                {

                        throw new ModelStateException();
                    
                }
                catch (ModelStateException modelStateException)
                {
                    string myError = "Invalid modelstate: ";
                    MyLogger.LogException(myError, modelStateException);
                    return NotFound(myError + modelStateException.Message);
                }

                // Validate the car object
                try
                {
                    if (!ModelState.IsValid)
                    {
                        throw new ModelStateException();
                    }
                }
                catch (ModelStateException modelStateException)
                {
                    string myError = "Invalid modelstate: ";
                    MyLogger.LogException(myError, modelStateException);
                    return NotFound(myError + modelStateException.Message);
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
                string myError = "Failed to add a car, unknown error:";
                MyLogger.LogException(myError, exception);
                return NotFound(myError + exception.Message);
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
                var file = formCollection.Files.First();

                try
                {
                    if (file.Length == 0)
                    {
                        throw new ImageAddingException();
                    }
                }
                catch (ImageAddingException ImageAddingException)
                {
                    string myError = "Failed to upload a car image, file was empty:";
                    MyLogger.LogException(myError, ImageAddingException);
                    return NotFound(myError + ImageAddingException.Message);
                }

                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
                var fullPath = Path.Combine(Paths.GetGlobalPath(), fileName.ToString());
                var dbPath = Path.Combine(Paths.GetLocalPath(), fileName.ToString());

                try
                {
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                catch (ImageAddingException imageAddingException)
                {
                    string myError = "Failed to upload a car image:";
                    MyLogger.LogException(myError, imageAddingException);
                    return NotFound(myError + imageAddingException.Message);
                }
                return Created("Car image uploaded successfully!", new { file });

            }
            catch (Exception exception)
            {
                string myError = "Failed to upload a car image, unknown error:";
                MyLogger.LogException(myError, exception);
                return NotFound(myError + exception.Message);
            }

        }



    }
}

