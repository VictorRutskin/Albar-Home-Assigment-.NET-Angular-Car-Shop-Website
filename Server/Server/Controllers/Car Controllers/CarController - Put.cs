using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Helpers;
using Server.Models;
using System;
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
                    string myError = "Car with id: " + id.ToString() + " could not be updated, ";
                    MyLogger.LogException(myError, notFoundInDbException);
                    return NotFound(myError + notFoundInDbException.Message);                
                }
                car.Name = UpdatedCar.Name;
                car.Category = UpdatedCar.Category;
                car.Price = UpdatedCar.Price;
                car.UnitsInStock = UpdatedCar.UnitsInStock;
                car.ModelYear = UpdatedCar.ModelYear;

                try
                {
                    await mydbcontext.SaveChangesAsync();
                }
                catch (DbActionFailedException dbActionFailedException)
                {
                    string myError = "failed to save car changes: ";
                    MyLogger.LogException(myError, dbActionFailedException);
                    return NotFound(myError + dbActionFailedException.Message);
                }

                return Ok(car);
            }
            catch (Exception exception)
            {
                string myError = "Failed to Update a car, unknown error:";
                MyLogger.LogException(myError, exception);
                return NotFound(myError + exception.Message);
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
                    string myError = "Car with id: " + id.ToString() + " could not be bought, ";
                    MyLogger.LogException(myError, notFoundInDbException);
                    return NotFound(myError + notFoundInDbException.Message);
                }


                if (car.UnitsInStock == 0)
                {
                    return BadRequest("Invalid Request: " + ModelState);
                }
                car.UnitsInStock = car.UnitsInStock - 1;

                try
                {
                    await mydbcontext.SaveChangesAsync();
                }
                catch (DbActionFailedException dbActionFailedException)
                {
                    string myError = "failed to save car changes: ";
                    MyLogger.LogException(myError, dbActionFailedException);
                    return NotFound(myError + dbActionFailedException.Message);
                }
                return Ok(car);

            }
            catch (Exception exception)
            {
                string myError = "Failed to buy a car, unknown error:";
                MyLogger.LogException(myError, exception);
                return NotFound(myError + exception.Message);
            }
        }


    }
}

