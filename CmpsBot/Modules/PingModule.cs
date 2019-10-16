using System.Threading.Tasks;
using Discord.Commands;

namespace CmpsBot.Modules
{
    public class PingModule : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public Task PingAsync() => ReplyAsync("Pong!");
    }
}