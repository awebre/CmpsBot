using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;




namespace CmpsBot.Modules
{
    [RequireContext(ContextType.Guild)]
    public class RoleModule : ModuleBase<SocketCommandContext>
    {
        private readonly IConfigurationRoot config;
        public RoleModule(IConfigurationRoot config)
        {
            this.config = config;
        }

        [Command("IT")]
        public async Task ITRoleTask()
        {
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == Roles.IT);
            await ((SocketGuildUser)Context.User).AddRoleAsync(role);
            await ReplyAsync("Role set as IT.");
        }
    }
}