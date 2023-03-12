using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using static Server.Helpers.ExceptionHandler;

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
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Request: " + ModelState);
                }

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
                    return NotFound("Car with id: " + id.ToString() + " could not be updated, " + notFoundInDbException.Message);
                }
                car.Name = UpdatedCar.Name;
                car.Category = UpdatedCar.Category;
                car.Price = UpdatedCar.Price;
                car.UnitsInStock = UpdatedCar.UnitsInStock;
                car.ModelYear = UpdatedCar.ModelYear;

                await mydbcontext.SaveChangesAsync();

                return Ok(car);
            }
            catch (Exception exception)
            {
                return StatusCode(500, "Failed to Update a car, unknown error" + exception);
            }

        }

        // Updates car values
        [HttpPut]
        [Route("Buy/{id:}")]
        public async Task<IActionResult> BuyCar([FromRoute] long id)
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
                    return NotFound("Car with id: " + id.ToString() + " could not be bought, " + notFoundInDbException.Message);
                }


                if (car.UnitsInStock == 0)
                {
                    return BadRequest("Invalid Request: " + ModelState);
                }
                car.UnitsInStock = car.UnitsInStock - 1;

                await mydbcontext.SaveChangesAsync();

                return Ok(car);

            }
            catch (Exception exception)
            {
                return StatusCode(500, "Failed to buy a car, unknown error" + exception);
            }
        }


    }
}

