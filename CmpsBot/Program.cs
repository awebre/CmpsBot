using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace CmpsBot
{
    class Program
    {
        private DiscordSocketClient client;
        private IConfiguration config;
        
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile("appsettings.dev.json", true, true)
                .Build();

            client = new DiscordSocketClient();
            client.Log += Log;

            await client.LoginAsync(TokenType.Bot, config["discordToken"]);
            await client.StartAsync();
            
            client.MessageReceived += MessageReceived;

            await Task.Delay(-1);
        }
        
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        
        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Content == "!ping")
            {
                await message.Channel.SendMessageAsync("Pong!");
            }
        }
    }
}
