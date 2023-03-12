using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Controllers
{
    //// PUT Requests
    public partial class CarController : Controller
    {
        // Updates car values
        [HttpPut]
        [Authorize]
        [Route("{id:}")]
        public async Task<IActionResult> UpdateCar([FromRoute] long id, Car UpdatedCar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Request: " + ModelState);
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

        // Updates car values
        [HttpPut]
        [Route("Buy/{id:}")]
        public async Task<IActionResult> BuyCar([FromRoute] long id)
        {
            var car = await mydbcontext.Cars.FirstOrDefaultAsync(x => x.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            if(car.UnitsInStock==0)
            {
                return BadRequest("Invalid Request: " + ModelState);
            }
            car.UnitsInStock = car.UnitsInStock-1;

            await mydbcontext.SaveChangesAsync();

            return Ok(car);
        }


    }
}

