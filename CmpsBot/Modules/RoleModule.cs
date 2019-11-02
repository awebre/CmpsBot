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
        
        //INSERT ROLE ID HERE:
        ulong ITrole;
        ulong CSrole;
        ulong MathMinorRole;

        [Command("ITRole")]
        public async Task ITRoleTask()
        {
            var role = Context.Guild.GetRole(ITrole);
            await ((SocketGuildUser)Context.User).AddRoleAsync(role);
            await ReplyAsync("Role set as IT.");
        }

        [Command("Computer Science")]
        public async Task CSRoleTask()
        {
            var role = Context.Guild.GetRole(CSrole);
            await ((SocketGuildUser)Context.User).AddRoleAsync(role);
            await ReplyAsync("Role set as CS.");
        }

        [Command("Math Minor")]
        public async Task MathMinorTask()
        {
            var role = Context.Guild.GetRole(MathMinorRole);
            await ((SocketGuildUser)Context.User).AddRoleAsync(role);
            await ReplyAsync("Role set as Math Minor.");
        }
    }
}