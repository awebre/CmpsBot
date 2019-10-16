using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CmpsBot.Configuration
{
    public class EventHandler
    {
        private readonly DiscordSocketClient discord;

        public EventHandler(DiscordSocketClient discord)
        {
            this.discord = discord;
            this.discord.UserJoined += NewMemberPrompt;
        }

        private async Task NewMemberPrompt(SocketGuildUser user)
        {
            var guild = user.Guild;
            var channel = guild.DefaultChannel;
            await channel.SendMessageAsync($"Welcome, {user.Mention}!\nPlease change your nickname in the server to your first and last name." +
                $" \nAlso, select your major with an emote reaction.");
        }
    }
}
