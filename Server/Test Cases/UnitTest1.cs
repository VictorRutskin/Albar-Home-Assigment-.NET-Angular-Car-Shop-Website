using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Server.Controllers;
using Server.Data;
using Server.Models;
using System.Collections.Generic;

namespace Test_Cases
{
    [TestFixture]
    public class PrimeService_IsPrimeShould
    {
        private MyDbContext mydbcontext;
        private CarController _controller;
        [SetUp]
        public void SetUp()
        {
        mydbcontext = new MyDbContext();
            _controller = new CarController(mydbcontext);
        }

        [Test]
        public void DbConnection() // Checks if connection to db works
        {
            // Add your test code here
        }

        [Test]
        public void AllAPIRequests() // Checks if all requests work
        {
            // Add your test code here
        }

        [Test]
        public void UserLogin() // Checks if login works
        {
            // Add your test code here
        }

        [Test]
        public async Task GetAllCars_ReturnsAllCars()
        {
            // Act
            var result = await _controller.GetAllCars();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            if (okResult == null)
            {
                Assert.Throws<System.Exception>(() => throw new System.Exception("GetAllCarsError."));
            }
        }
    }

}
