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

            [HttpGet]
            public async Task<IActionResult> GetAllCars()
            {
                var employees = await mydbcontext.Cars.ToListAsync();

                return Ok(employees);
            }

            [HttpPost]
            public async Task<IActionResult> AddCar([FromBody] Car car)
            {

                await mydbcontext.AddAsync(car);
                await mydbcontext.SaveChangesAsync();

                return Ok(car);
            }


            [HttpGet]
            [Route("{id:}")]

            public async Task<IActionResult> GetCar([FromRoute] long id)
            {
                var employee = await mydbcontext.Cars.FirstOrDefaultAsync(x => x.Id == id);

                if (employee == null)
                {
                    return NotFound();
                }

                return Ok(employee);
            }

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

        }
    }

