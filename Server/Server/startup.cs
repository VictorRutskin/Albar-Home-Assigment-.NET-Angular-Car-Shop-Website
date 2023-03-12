using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Server.Helpers;
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
            ConfiguredValues configuredValues = new ConfiguredValues();
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyHeader()
                           .AllowAnyMethod()
                           .WithOrigins(configuredValues.GetClient(), configuredValues.GetServer());
                });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                ConfiguredValues configuredValues = new ConfiguredValues();

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Validate the token's issuer, audience, expiration, and key
                    ValidateIssuer = true, // Server created the token
                    ValidateAudience = true, // Reciever is valid
                    ValidateLifetime = true, // The token has not expired
                    ValidateIssuerSigningKey = true, // key is valid

                    ValidIssuer = configuredValues.GetServer(), // issuer is this host
                    ValidAudience = configuredValues.GetClient(), // Audience is the default angular port
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuredValues.GetSecretKey())) // Secret key
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

            // Configuring CORS to allow requests from your Angular app on port 4200
            app.UseCors("EnableCORS");

            // Serving static files from the wwwroot folder
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
                RequestPath = env.WebRootPath
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
