using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Helpers;

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
                System.IO.File.Delete(imagePath);
            }

            return NoContent();
        }


    }
}

