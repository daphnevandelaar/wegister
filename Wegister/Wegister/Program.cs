using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Wegister.DAL;

namespace Wegister
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dbOptions = new DbContextOptionsBuilder<WegisterDbContext>()
                .UseSqlServer(Environment.GetEnvironmentVariable("WEGISTERDB_CONNECTIONSTRING"))
                .Options;

            TryEnsureCreated(dbOptions);
            CreateHostBuilder(args).Build().Run();
        }

        private static void TryEnsureCreated(DbContextOptions<WegisterDbContext> dbOptions)
        {
            try
            {
                using (var context = new WegisterDbContext(dbOptions))
                {
                    try
                    {
                        Console.WriteLine("Print migrations: " + context.Database.GetAppliedMigrations().Count());
                        foreach(var migration in context.Database.GetAppliedMigrations())
                        {
                            Console.WriteLine("Migration applied: " + migration);
                        }
                        Console.WriteLine("------------------------------");
                        context.Database.Migrate();
                        Console.WriteLine("Database migrated");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception thrown: " + e);
                        context.Database.EnsureCreated();
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("TryEnsureCreated error before connection open");
                Console.WriteLine(e);
                Console.WriteLine("------------------------------");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
