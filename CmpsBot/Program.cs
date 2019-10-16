using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace CmpsBot
{
    public class Program
    {
        public static void Main(string[] args) => Startup.RunAsync(args).GetAwaiter().GetResult();
    }
}
