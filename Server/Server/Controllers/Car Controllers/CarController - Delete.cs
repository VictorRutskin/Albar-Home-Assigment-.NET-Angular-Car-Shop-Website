using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Server.Data;
using Server.Models;
using Server.Services;
using System.Web;
using static Server.Services.ExceptionHandler;

namespace Server.Controllers
{
    public partial class CarController : Controller
    {

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

            return NoContent();
        }


    }
}

