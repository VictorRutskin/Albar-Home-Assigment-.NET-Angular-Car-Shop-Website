using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Server.Services;
using System.Text;

namespace YourWebApiNamespace
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Valid if:
                    ValidateIssuer = true, // Server created the token
                    ValidateAudience = true, // Reciever is valid
                    ValidateLifetime = true, // The token has not expired
                    ValidateIssuerSigningKey = true, // key is valid

                    ValidIssuer = "https://localhost:7099", // issuer is this host
                    ValidAudience = "http://localhost:4200", // Audience is the default angular port
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfiguredValues.GetSecretKey()))  // Secret key
                };
            });

            services.AddControllers();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Configuring Cors, it will only allow requests from your angular local port: 4200.
            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(ConfiguredValues.GetClient(), ConfiguredValues.GetServer()));

            // To use my wwwroot static files
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")),
                RequestPath = new PathString("/wwwroot")
            });

            app.UseHttpsRedirection();


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
