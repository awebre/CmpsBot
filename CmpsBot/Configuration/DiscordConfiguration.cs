using System;
using System.Reflection;
using System.Threading.Tasks;
using CmpsBot.Modules;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CmpsBot.Configuration
{
    public static class DiscordConfiguration
    {
        public static async Task ConfigureAndRun(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var discordToken = configuration["discordToken"];
            if (string.IsNullOrEmpty(discordToken))
            {
                throw new Exception("Please ensure your discord token is supplied in either appsettings.json or appsettings.dev.json");
            }

            var discord = serviceProvider.GetRequiredService<DiscordSocketClient>();
            await discord.LoginAsync(TokenType.Bot, discordToken);
            await discord.StartAsync();

            var commands = serviceProvider.GetRequiredService<CommandService>();
            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), serviceProvider);
        }
    }
}