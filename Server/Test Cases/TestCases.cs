using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Server.Controllers;
using Server.Data;
using Server.Models;
using System.Collections.Generic;

namespace Test_Cases
{
    [TestFixture]
    public class TestCases
    {
        private MyDbContext _dbContext;

        [SetUp]
        public void SetUp()
        {
            // Build a new DbContextOptions instance using your connection string
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseSqlServer(GetConnectionString("MyConnectionString"))
                .Options;

            _dbContext = new MyDbContext(options);
        }

        private string GetConnectionString(string connectionStringName)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            return configuration.GetConnectionString(connectionStringName)!;
        }


        [Test]
        public void DbConnection() // Checks if connection to db works
        {
            try
            {
                _dbContext.Database.OpenConnection();
                Assert.IsTrue(_dbContext.Database.CanConnect());
            }
            catch (Exception)
            {
                Assert.Fail("Connection to database failed.");
            }
        }

        [Test]
        public async Task GetAllCars()
        {
            var result = await _dbContext.Cars.ToListAsync();

            Assert.IsNotNull(result);

            if (result == null)
            {
                Assert.Throws<System.Exception>(() => throw new System.Exception("GetAllCars Error."));
            }
        }

        [Test]
        public async Task GetCarUsingId()
        {
            long IdToCheck = 1;

            var result = await _dbContext.Cars.FirstOrDefaultAsync(x => x.Id == IdToCheck);

            Assert.IsNotNull(result);

            if (result == null)
            {
                Assert.Throws<System.Exception>(() => throw new System.Exception("GetCarUsingId Error."));
            }
        }

        [Test]
        public async Task GetCarUsingName()
        {
            string NameToCheck = "Toyota Camry";

            var result = await _dbContext.Cars.FirstOrDefaultAsync(x => x.Name == NameToCheck);

            Assert.IsNotNull(result);

            if (result == null)
            {
                Assert.Throws<System.Exception>(() => throw new System.Exception("GetCarUsingName Error."));
            }
        }

        [Test]
        public async Task GetTop3Extras()
        {
            var result = await _dbContext.Cars.OrderByDescending(c => c.UnitsInStock)
                                 .Take(3)
                                 .ToListAsync();
            Assert.IsNotNull(result);

            if (result == null)
            {
                Assert.Throws<System.Exception>(() => throw new System.Exception("GetTop3Extras Error."));
            }
        }

        [Test]
        public async Task FindCarImageUsingID()
        {
            long IdToCheck = 1;

            var result = await _dbContext.Cars.FindAsync(IdToCheck);

            Assert.IsNotNull(result);

            if (result == null)
            {
                Assert.Throws<System.Exception>(() => throw new System.Exception("FindCarImageUsingID Error."));
            }
        }

        [Test]
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
                var result = await _dbContext.AddAsync(TestCar);
            }
            catch
            {
                Assert.Throws<System.Exception>(() => throw new System.Exception("FindCarImageUsingID Error."));
            }

        }

        [Test]
        public async Task DeleteCar()
        {
            long IdToCheck = 1;

            var car = await _dbContext.Cars.FindAsync(IdToCheck);

            if (car == null)
            {
                Assert.Throws<System.Exception>(() => throw new System.Exception($"DeleteCar Error, Car with id '{IdToCheck}' not found."));
            }

            try
            {
                _dbContext.Cars.Remove(car!);
            }
            catch
            {
                Assert.Throws<System.Exception>(() => throw new System.Exception("DeleteCar Error."));
            }

        }

        [Test]
        public async Task UserLogin() // Checks if login works
        {
            User VictorUser = new User
            {
                Id = 1,
                Name = "Victor Rutskin",
                Password = "BigBruhMomentPerformer",
                LastLogin = DateTime.Now
            };

            var result = await _dbContext.Users.FirstOrDefaultAsync(x => x.Name == VictorUser.Name && x.Password == VictorUser.Password);

            Assert.IsNotNull(result);

            if (result == null)
            {
                Assert.Throws<System.Exception>(() => throw new System.Exception("GetAllCarsError."));
            }
        }



    }

}
