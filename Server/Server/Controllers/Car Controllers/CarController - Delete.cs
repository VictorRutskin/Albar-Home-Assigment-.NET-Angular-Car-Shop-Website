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
        public async Task<IActionResult> DeleteCar(long id)
        {
            try
            {
                var car = await mydbcontext.Cars.FindAsync(id);

                if (car == null)
                {
                    throw new NotFoundInDbException($"Car with id '{id}' not found.");
                }

                mydbcontext.Cars.Remove(car);
                await mydbcontext.SaveChangesAsync();

                string imagePath = Paths.GetLocalPath() + @"\" + car.ImageSrc!;
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                return NoContent();
            }
            catch (NotFoundInDbException exception)
            {
                string myError = "Not found in DataBase";
                MyLogger.LogException(myError, exception);
                return NotFound(myError);
            }
            catch (DbUpdateException exception)
            {
                string myError = "Failed to delete a car, unknown error:";
                MyLogger.LogException(myError,exception);
                return BadRequest(myError);
            }
            catch (Exception exception)
            {
                string myError = "Unknown Error";
                MyLogger.LogException(myError, exception);
                return StatusCode(500, "An error occurred while deleting the car.");
            }
        }


    }
}

