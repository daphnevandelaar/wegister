using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Wegister.BLL;
using Wegister.DAL;
using Wegister.DAL.Infra;

namespace Wegister
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            WebHostEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            if (WebHostEnvironment.IsDevelopment())
            {
                services.AddSingleton<IAuthorizationHandler, AllowAnonymous>();
            }
            else
            {
                services.AddMvc();
                services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = Environment.GetEnvironmentVariable("ISSUER_NAME");
                    options.RequireHttpsMetadata = false;

                    options.Audience = "wegister-web";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuers = new string[]
                        {
                            Environment.GetEnvironmentVariable("ISSUER_NAME")
                        }
                    };
                });
            }

            services.AddTransient<IHourRegistrationService, HourRegistrationService>();
            services.AddTransient<IHourRegistrationExtensionWorkweekService, WorkweekService>();
            services.AddTransient<IWorkweekService, WorkweekService>();
            services.AddTransient<IEmployerService, EmployerService>();
            services.AddTransient<IHourRegistrationExtensionEmployerService, EmployerService>();
            services.AddTransient<IEmployerExtensionHourRegistrationService, HourRegistrationService>();

            services.AddTransient<IHourRegistrationLogic, HourRegistrationLogic>();
            services.AddTransient<IWorkWeekCalculation, WorkWeekCalculation>();
            services.AddTransient<IWorkWeekLogic, WorkWeekLogic>();
            services.AddTransient<IEmployerLogic, EmployerLogic>();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connstring = Configuration["WEGISTERDB_CONNECTIONSTRING"] ?? configuration.GetConnectionString("WEGISTERDB_CONNECTIONSTRING");
            services.AddDbContext<WegisterDbContext>(options => options.UseSqlServer(connstring));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
