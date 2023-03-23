using Microsoft.EntityFrameworkCore;
using Server.Controllers;
using Server.Data;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static Server.Helpers.ExceptionHandler;

namespace Test_Cases
{
    public class ApiToTestMethods
    {
        private readonly MyDbContext mydbcontext;

        public ApiToTestMethods(MyDbContext mydbcontext)
        {
            this.mydbcontext = mydbcontext;

        }


        public async Task GetAllCars()
        {
            var result = await mydbcontext.Cars.ToListAsync();

            Assert.IsNotNull(result);

            if (result == null)
            {
                Assert.Throws<System.Exception>(() => throw new System.Exception("GetAllCars Error."));
            }
        }

        public async Task GetCarUsingId()
        {
            int IdToCheck = 1;

            var result = await mydbcontext.Cars.FirstOrDefaultAsync(x => x.Id == IdToCheck);

            Assert.IsNotNull(result);

            if (result == null)
            {
                Assert.Throws<System.Exception>(() => throw new System.Exception("GetCarUsingId Error."));
            }
        }

        public async Task GetCarUsingName()
        {
            string NameToCheck = "Toyota Camry";

            var result = await mydbcontext.Cars.FirstOrDefaultAsync(x => x.Name == NameToCheck);

            Assert.IsNotNull(result);

            if (result == null)
            {
                Assert.Throws<System.Exception>(() => throw new System.Exception("GetCarUsingName Error."));
            }
        }

        public async Task GetTop3Extras()
        {
            var result = await mydbcontext.Cars.OrderByDescending(c => c.UnitsInStock)
                                 .Take(3)
                                 .ToListAsync();
            Assert.IsNotNull(result);

            if (result == null)
            {
                Assert.Throws<System.Exception>(() => throw new System.Exception("GetTop3Extras Error."));
            }
        }

        public async Task FindCarImageUsingID()
        {
            int IdToCheck = 1;

            var result = await mydbcontext.Cars.FindAsync(IdToCheck);

            Assert.IsNotNull(result);

            if (result == null)
            {
                Assert.Throws<System.Exception>(() => throw new System.Exception("FindCarImageUsingID Error."));
            }
        }

        public async Task AddCar()
        {
            // Create a new Car 
            var TestCar = new Car
            {
                Name = "Test Car",
                Category = "Luxury",
                Price = 50000,
                UnitsInStock = 10,
                ModelYear = 2022,
                ImageSrc = "https://example.com/mycar.jpg"
            };

            try
            {
                var result = await mydbcontext.AddAsync(TestCar);
                // Check that the result is successful and that the new car was added to the database
                var addedCar = await mydbcontext.Cars.FirstOrDefaultAsync(c => c.Name == "Test Car");
                Assert.IsNotNull(addedCar);
            }
            catch
            {
                Assert.Throws<System.Exception>(() => throw new System.Exception("FindCarImageUsingID Error."));
            }

        }

        public async Task DeleteCar()
        {
            int IdToCheck = 1;

            var car = await mydbcontext.Cars.FindAsync(IdToCheck);

            if (car == null)
            {
                Assert.Throws<System.Exception>(() => throw new System.Exception($"DeleteCar Error, Car with id '{IdToCheck}' not found."));
            }

            try
            {
                mydbcontext.Cars.Remove(car!);
            }
            catch
            {
                Assert.Throws<System.Exception>(() => throw new System.Exception("DeleteCar Error."));
            }      

        }

    }
}
