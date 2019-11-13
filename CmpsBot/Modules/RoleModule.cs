using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using CmpsBot.Utils;
using CmpsBot.Services;
using System.Linq;
using System.Collections.Generic;
using System;
using Discord;

namespace CmpsBot.Modules
{
    [RequireContext(ContextType.Guild)]
    public class RoleModule : ModuleBase<SocketCommandContext>
    {
        private readonly IConfigurationRoot config;
        private SyncSet anonymouseRoles;
        public RoleModule(IConfigurationRoot config, FileSystemService fileSystemService)
        {
            this.config = config;
            this.anonymouseRoles = fileSystemService.GetFile(File.AnonymouseRoles);
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

        [RequireUserPermission(Discord.GuildPermission.Administrator)]
        [Command("ConfigureRole")]
        public async Task ConfigureRoleTask(params string[] roles) {
            var guildRoles = Context.Guild.Roles;
            foreach(var role in roles) {
                foreach(var gr in guildRoles.Where(x => role.Equals(x.Name, System.StringComparison.InvariantCultureIgnoreCase))) {
                    anonymouseRoles.Add(gr.Id.ToString());
                    await ReplyAsync($"Added {gr.Name} to be anonymously assignable");
                }
            }
        }

        [RequireUserPermission(Discord.GuildPermission.Administrator)]
        [Command("DeleteRole")]
        public async Task DeleteRoleTask(params string[] roles) {
            var guildRoles = Context.Guild.Roles;
            foreach(var role in roles) {
                foreach(var gr in guildRoles.Where(x => role.Equals(x.Name, System.StringComparison.InvariantCultureIgnoreCase))) {
                    anonymouseRoles.Remove(gr.Id.ToString());
                    await ReplyAsync($"Removed {gr.Name} from being anonymously assignable");
                }
            }
        }

        [Command("AddRole")]
        public async Task AddRoleTask(params string[] roles) {
            var lowerRoles = roles.Select(x => x.ToLower());
            var knownRoles = anonymouseRoles.Get();
            var intendedRolesInGuild = Context.Guild.Roles.Where(x => roles.Contains(x.Name.ToLower()));
            var rolesToAdd = intendedRolesInGuild.Where( x => knownRoles.Contains(x.Id.ToString()));
            await (Context.User as IGuildUser).AddRolesAsync(rolesToAdd);
            await ReplyAsync($"Added {string.Join(", ", rolesToAdd)} to {(Context.User as IGuildUser).Nickname}");
        }

        [Command("RemoveRole")]
        public async Task RemoveRoleTask(params string[] roles) {
            var lowerRoles = roles.Select(x => x.ToLower());
            var knownRoles = anonymouseRoles.Get();
            var intendedRolesInGuild = Context.Guild.Roles.Where(x => roles.Contains(x.Name.ToLower()));
            var rolesToRemove = intendedRolesInGuild.Where( x => knownRoles.Contains(x.Id.ToString()));
            await (Context.User as IGuildUser).RemoveRolesAsync(rolesToRemove);
            await ReplyAsync($"Removed {string.Join(", ", rolesToRemove)} from {(Context.User as IGuildUser).Nickname}");
        }

        [Command("ListRoles")]
        public async Task ListRolesTask() {
            var knownRoles = anonymouseRoles.Get();
            var guildRoles = Context.Guild.Roles;
            var namedRoles = new List<string>();
            foreach(var role in knownRoles) {
                var foundRole = guildRoles.FirstOrDefault(x => x.Id == Convert.ToUInt64(role));
                if(foundRole.Name != "") {
                    namedRoles.Add(foundRole.Name);
                }
            }
            await ReplyAsync(string.Join(", ", namedRoles));
        }
    }
}