using Escape.Mines.Application;
using Escape.Mines.Application.GameSettings;
using Escape.Mines.Infrastructure;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Escape.Mines.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using(var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var game = services.GetRequiredService<Game>();
                    await game.Start();
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while attepting to start the game.");
                }
            }

            await host.RunAsync();


        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
            .ConfigureLogging((context,logging) =>
            {
                logging.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Warning);

            })
            .ConfigureHostConfiguration(configHost =>
             {
                 configHost.SetBasePath(Directory.GetCurrentDirectory());
                 configHost.AddJsonFile("appsettings.json", optional: false);
                 configHost.AddEnvironmentVariables();
             })
             .ConfigureServices((hostContext, services) =>
              {
                  services.AddOptions()
                  .Configure<ConfigurationData>(hostContext.Configuration.GetSection("ConfigurationData"));
                  services.AddInfrastructure();
                  services.AddApplication();
                  services.AddValidatorsFromAssemblyContaining(typeof(GameSettingsDto));
                  services.AddTransient<Game>();

              });
            
    }
}
