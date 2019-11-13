using System;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CmpsBot.Services;

namespace CmpsBot.Configuration
{
    public static class ServiceConfiguration
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
                {                                       // Add discord to the collection
                    LogLevel = LogSeverity.Verbose,     // Tell the logger to give Verbose amount of info
                    MessageCacheSize = 1000             // Cache 1,000 messages per channel
                }))
                .AddSingleton(new CommandService(new CommandServiceConfig
                {                                       // Add the command service to the collection
                    LogLevel = LogSeverity.Verbose,     // Tell the logger to give Verbose amount of info
                    DefaultRunMode = RunMode.Async,     // Force all commands to run async by default
                }))
                .AddSingleton<CommandHandler>()         // Add the command handler to the collection
                .AddSingleton<EventHandler>()           // Add the event handler to the collection
                .AddSingleton<Random>()                 // Add random to the collection
                .AddSingleton((IConfigurationRoot) configuration)
                .AddSingleton<FileSystemService>();           // Add the configuration to the collection
        }
    }
}