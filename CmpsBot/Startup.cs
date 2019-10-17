using System;
using System.Threading.Tasks;
using CmpsBot.Configuration;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EventHandler = CmpsBot.Configuration.EventHandler;

namespace CmpsBot
{
    public class Startup
    {
                public IConfigurationRoot Configuration { get; }

        public Startup(string[] args)
        {
            var builder = new ConfigurationBuilder()                                            // Create a new instance of the config builder
                .AddJsonFile("appsettings.json", false, true)     // Add this required json file to the configuration
                .AddJsonFile("appsettings.dev.json", true, true); // Add this optional json file to the configuration
            Configuration = builder.Build();                                                    // Build the configuration
        }

        public static async Task RunAsync(string[] args)
        {
            var startup = new Startup(args);
            await startup.RunAsync();
        }

        public async Task RunAsync()
        {
            var services = new ServiceCollection();                   // Create a new instance of a service collection
            ServiceConfiguration.Configure(services, Configuration);  // Add services to service collection

            var provider = services.BuildServiceProvider();           // Build the service provider
            provider.GetRequiredService<CommandHandler>(); 		      // Start the command handler service
            provider.GetRequiredService<EventHandler>();              // Start the event handler service


            await DiscordConfiguration.ConfigureAndRun(provider, Configuration); //Configures and starts the discord socket client

            await Task.Delay(-1);                                     // Keep the program alive
        }
    }
}