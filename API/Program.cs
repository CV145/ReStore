using System;
using API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        /*
        When dotnet run starts it will look for Main()
        */
        public static void Main(string[] args)
        {
            /*
            Building .NET's default server to host the API
            */
            var host = CreateHostBuilder(args).Build();

            //The using keyword deletes the variable when it is no longer being used
            using var scope = host.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                //Will update the database or create a new one
                context.Database.Migrate();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Problem migrating data");
            }

            //Run the application
            host.Run();
        }

        /*
        Starts the server up.
        */
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //Startup class
                    webBuilder.UseStartup<Startup>();
                });
    }
}
