using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Helpers


{
    // Configuration interface, for implemantation
    public interface IConfiguredValues
    {
        string GetSecretKey();
        string GetClient();
        string GetServer();
        JwtSecurityToken GetToken();
    }

    // Class to define configurations values for this project
    public class ConfiguredValues : IConfiguredValues
    {
        private readonly IConfiguration _configuration;

        public ConfiguredValues(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetSecretKey()
        {
            return "123BossVictor123";
        }

        public string GetClient()
        {
            return _configuration.GetValue<string>("ConfiguredValues:Client");
        }

        public string GetServer()
        {
            return _configuration.GetValue<string>("ConfiguredValues:Server");
        }


        // Configuring token options
        public JwtSecurityToken GetToken()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetSecretKey()));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: GetServer(),
                audience: GetClient(),
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCredentials
                );

            return tokenOptions;

        }
    }
}
