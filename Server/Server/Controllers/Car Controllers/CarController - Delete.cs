using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Helpers;
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
                    return NotFound("Car with id: "+id.ToString()+", " + notFoundInDbException.Message);
                }

                mydbcontext.Cars.Remove(car);
                await mydbcontext.SaveChangesAsync();

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
                return StatusCode(500, "Failed to delete a car, unknown error" + exception);
            }
        }


    }
}

