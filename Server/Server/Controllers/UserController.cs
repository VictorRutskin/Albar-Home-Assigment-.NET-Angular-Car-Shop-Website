using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.Data;
using Server.Models;
using Server.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly MyDbContext mydbcontext;


        public UserController(MyDbContext mydbcontext)
        {
            this.mydbcontext = mydbcontext;

        }

        // Checks if login request is good, if good returns token for the user to use as specific page authorization
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] User userRequest)
        {
            if(userRequest == null)
            {
                return BadRequest("Invalid Request");
            }

            var user = await mydbcontext.Users.FirstOrDefaultAsync(x => x.Name == userRequest.Name && x.Password == userRequest.Password);

            if(user == null) 
            {
                return Unauthorized("No user was found");
            }

            // Configuring token and its assets
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfiguredValues.GetSecretKey()));
            var signingCredentials = new SigningCredentials(secretKey,SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: ConfiguredValues.GetServer(),
                audience: ConfiguredValues.GetClient(),
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCredentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            // updating login to now
            user.LastLogin = DateTime.Now;
            await mydbcontext.SaveChangesAsync();

            return Ok(new {Token = tokenString});
        }

    }
}
