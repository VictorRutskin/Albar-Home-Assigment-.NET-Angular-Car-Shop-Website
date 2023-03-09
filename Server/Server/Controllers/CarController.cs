using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : Controller
    {
            private readonly MyDbContext mydbcontext;

            public CarController(MyDbContext mydbcontext)
            {
                this.mydbcontext = mydbcontext;
            }

            //// POST
            //
            // Adds a car with specific values
            [HttpPost]
            public async Task<IActionResult> AddCar([FromBody] Car car)
            {

                await mydbcontext.AddAsync(car);
                await mydbcontext.SaveChangesAsync();

                return Ok(car);
            }
            //
            //
            ////


            //// GET 
            //
            // Returns all cars
            [HttpGet]
            public async Task<IActionResult> GetAllCars()
            {
                var cars = await mydbcontext.Cars.ToListAsync();

                return Ok(cars);
            }

            // Returns a specific car
            [HttpGet]
            [Route("{id:}")]
            public async Task<IActionResult> GetCar([FromRoute] long id)
            {
                var car = await mydbcontext.Cars.FirstOrDefaultAsync(x => x.Id == id);

                if (car == null)
                {
                    return NotFound();
                }

                return Ok(car);
            }

            // Returns top 3 cars with most UnitsInStock value
            [HttpGet]
            [Route("{id:}")]
            public async Task<IActionResult> Get3CarExtras()
            {
                var cars = await mydbcontext.Cars.OrderByDescending(c => c.UnitsInStock)
                                                 .Take(3)
                                                 .ToListAsync();

                if (cars == null || !cars.Any())
                {
                    return NotFound();
                }

                return Ok(cars);
            }
            //
            //
            ////


            //// PUT 
            //
            // Updates car values
            [HttpPut]
            [Route("{id:}")]
            public async Task<IActionResult> UpdateCar([FromRoute] long id, Car UpdatedCar)
            {
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
            //
            //
            ////


            //// DELETE 
            //
            // Deletes a car using id
            [HttpDelete]
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

                return Ok(car);
            }
            //
            //
            ////

        }
    }

