using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Helpers;
using System;
using static Server.Helpers.ExceptionHandler;

namespace Server.Controllers
{
    //// DELETE Requests
    public partial class CarController : Controller
    {
        // Deletes a car using id
        [HttpDelete]
        [Authorize]
        [Route("{id:}")]
        public async Task<IActionResult> DeleteCar([FromRoute] long id)
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
                    string myError = "Car with id: " + id.ToString() + ", ";
                    MyLogger.LogException(myError,notFoundInDbException);
                    return NotFound(myError + notFoundInDbException.Message);
                }
                try
                {
                    mydbcontext.Cars.Remove(car);
                    await mydbcontext.SaveChangesAsync();
                }
                catch (DbActionFailedException dbActionFailedException)
                {
                    string myError = "failed to remove a car from db: ";
                    MyLogger.LogException(myError, dbActionFailedException);
                    return NotFound(myError + dbActionFailedException.Message);
                }
                // If image exists delete, else dont
                string imagePath = Paths.GetLocalPath() + @"\" + car.ImageSrc!;
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                return NoContent();
            }
            catch (Exception exception)
            {
                string myError = "Failed to delete a car, unknown error:";
                MyLogger.LogException(myError, exception);
                return NotFound(myError + exception.Message);
            }
        }


    }
}

