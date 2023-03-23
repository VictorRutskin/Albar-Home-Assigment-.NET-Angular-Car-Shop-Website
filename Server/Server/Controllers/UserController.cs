using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.Data;
using Server.Models;
using Server.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Server.Helpers.ExceptionHandler;
using Microsoft.Extensions.Configuration;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly MyDbContext mydbcontext;
        private readonly IConfiguration _configuration;

        public UserController(MyDbContext mydbcontext, IConfiguration configuration)
        {
            this.mydbcontext = mydbcontext;
            _configuration = configuration;

        }

        // Checks if login request is good, if good returns token for the user to use as specific page authorization
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] User userRequest)
        {
            try
            {
                var user = await mydbcontext.Users.FirstOrDefaultAsync(x => x.Name == userRequest.Name && x.Password == userRequest.Password);

                if (user == null)
                {
                    throw new UnauthorizedUserException();
                }
                // updating login to now
                user.LastLogin = DateTime.Now;
                await mydbcontext.SaveChangesAsync();

                ConfiguredValues configuredValues = new ConfiguredValues(_configuration);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(configuredValues.GetToken());

                return Ok(new { Token = tokenString });
            }
            catch (UnauthorizedUserException exception)
            {
                string myError = userRequest.Name+ " Not found in DataBase";
                MyLogger.LogException(myError, exception);
                return Unauthorized(myError);
            }
            catch (Exception exception)
            {
                string myError = "Unknown Error";
                MyLogger.LogException(myError, exception);
                return StatusCode(500, "An error occurred while deleting the car.");
            }
        }


    }
}
