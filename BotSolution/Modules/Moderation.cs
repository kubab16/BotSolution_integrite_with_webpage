using BotSolution.Common;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Infrastructure;
using Infrastructure.Bot;
using Infrastructure.Languages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotSolution.Modules
{
    public class Moderation : ModuleBase<SocketCommandContext>
    {
        private readonly DiscordSocketClient _client;
        private readonly Comand _comand;
        private readonly ModerationRoles _ModerationRoles;
        private readonly Punishments _punishment;
        private readonly PunishmentRole _punishmentRole;
        private readonly Servers _server;
        private readonly Languages _language;

        public Moderation(DiscordSocketClient client,
                          Comand comand,
                          ModerationRoles moderation,
                          Punishments punishment,
                          PunishmentRole punishmentRole,
                          Servers server,
                          Languages languages)
        {
            _client = client;
            _comand = comand;
            _ModerationRoles = moderation;
            _punishment = punishment;
            _punishmentRole = punishmentRole;
            _server = server;
            _language = languages;
        }

        [Command("textmute")]
        [Alias("tmute")]
        [RequireBotPermission(Discord.GuildPermission.ManageRoles)]
        public async Task Tmute(SocketUser user, string time = null, string reason = null)
        {
            if (!await _comand.GetComendConfig(Context.Guild.Id, TypeComand.moderation)) return;
            bool hasPermision = false;
            var permision = (Context.User as SocketGuildUser).Roles.ToList();       
            for (int i = 0; i < permision.Count; i++)
            {
                if (permision[i].Permissions.Administrator
                    || await _ModerationRoles.RoleHasPermision(Context.Guild.Id, permision[i].Id, PermisionLevel.Modelators))
                {
                    hasPermision = true;
                    break;
                }
            }
            if (!hasPermision && !(Context.Guild.Owner == Context.User))
            {
                return;
            }
            if (await DateCalculator.AddTime(time) != null)
            {
                DateTime EndTime = (DateTime)await DateCalculator.AddTime(time);
                if (await TextMutes(user as SocketGuildUser))
                {
                    await SendMesageError(Context, "VMute");
                    return;
                }
                await _punishment.AddPunishmentUser(Context.Guild.Id, user.Id, typePunishment.textmute, EndTime, Context.User.Id, reason);
            }
            else
            {
                if( await TextMutes(user as SocketGuildUser))
                {
                    await SendMesageError(Context, "VMute");
                    return;
                }
                await _punishment.AddPunishmentUser(Context.Guild.Id, user.Id, typePunishment.textmute, Context.User.Id, reason);
            }

            await SendMesage(Context, user as SocketGuildUser, "TMute", time, reason, Discord.Color.DarkRed);
            await Context.Message.DeleteAsync();
        }

        [Command("voicemute")]
        [Alias("vmute")]
        [RequireBotPermission(Discord.GuildPermission.ManageRoles)]
        public async Task vmute(SocketUser user, string time = null, string reason = null)
        {
            if (!await _comand.GetComendConfig(Context.Guild.Id, TypeComand.moderation)) return;
            bool hasPermision = false;
            var permision = (Context.User as SocketGuildUser).Roles.ToList();
            for (int i = 0; i < permision.Count; i++)
            {
                if (permision[i].Permissions.Administrator
                    || await _ModerationRoles.RoleHasPermision(Context.Guild.Id, permision[i].Id, PermisionLevel.Modelators))
                {
                    hasPermision = true;
                    break;
                }
            }
            if (!hasPermision && !(Context.Guild.Owner == Context.User) ) return;
            if (await DateCalculator.AddTime(time) != null)
            {
                DateTime EndTime = (DateTime)await DateCalculator.AddTime(time);
                await _punishment.AddPunishmentUser(Context.Guild.Id, user.Id, typePunishment.vmute, EndTime, Context.User.Id, reason);
                if (!await VoiceMutes(user as SocketGuildUser))
                {
                    await SendMesageError(Context, "VMute");
                    return;
                }
            }
            else
            {
                await _punishment.AddPunishmentUser(Context.Guild.Id, user.Id, typePunishment.vmute, Context.User.Id, reason);
                if (!await VoiceMutes(user as SocketGuildUser))
                {
                    await SendMesageError(Context, "VMute");
                    return;
                }
            }

            await SendMesage(Context, user as SocketGuildUser, "Vmute", time, reason, Discord.Color.DarkRed);
            await Context.Message.DeleteAsync();
        }

        [Command("ban")]
        [RequireUserPermission(Discord.GuildPermission.BanMembers)]
        public async Task ban(SocketUser user, string time = null, string reason = null)
        {
            if (!await _comand.GetComendConfig(Context.Guild.Id, TypeComand.moderation)) return;
            bool hasPermision = false;
            var permision = (Context.User as SocketGuildUser).Roles.ToList();
            for (int i = 0; i < permision.Count; i++)
            {
                if (permision[i].Permissions.Administrator || await _ModerationRoles.RoleHasPermision(Context.Guild.Id, permision[i].Id, PermisionLevel.ModelatorOlders))
                {
                    hasPermision = true;
                    break;
                }
            }
            if (!hasPermision && !(Context.Guild.Owner == Context.User)) return;
            if (await DateCalculator.AddTime(time) != null)
            {
                DateTime EndTime = (DateTime)await DateCalculator.AddTime(time);
                await Context.Channel.SendMessageAsync(user.Mention);
                await Context.Guild.AddBanAsync(user, 0, reason);
                await _punishment.AddPunishmentUser(Context.Guild.Id, user.Id, typePunishment.ban, EndTime, Context.User.Id, reason);
            }
            else{
                await Context.Guild.AddBanAsync(user, 0, reason);
                await _punishment.AddPunishmentUser(Context.Guild.Id, user.Id, typePunishment.ban, Context.User.Id, reason);                
            }

            await SendMesage(Context, user as SocketGuildUser, "ban", time, reason, Discord.Color.Red);
            await Context.Message.DeleteAsync();
        }

        [Command("warn")]
        [RequireUserPermission(Discord.GuildPermission.BanMembers)]
        public async Task Warn(SocketUser user,  string reason)
        {
            if (!await _comand.GetComendConfig(Context.Guild.Id, TypeComand.moderation)) return;
            bool hasPermision = false;
            var permision = (Context.User as SocketGuildUser).Roles.ToList();
            for (int i = 0; i < permision.Count; i++)
            {
                if (permision[i].Permissions.Administrator || await _ModerationRoles.RoleHasPermision(Context.Guild.Id, permision[i].Id, PermisionLevel.Modelators))
                {
                    hasPermision = true;
                    break;
                }
            }
            if (!hasPermision && !(Context.Guild.Owner == Context.User)) return;
            await _punishment.AddPunishmentUser(Context.Guild.Id, user.Id, typePunishment.warn, Context.User.Id, reason);

            await SendMesage(Context, user as SocketGuildUser, "Vmute", reason, Discord.Color.Orange);
            await Context.Message.DeleteAsync();
        }

        [Command("kick")]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task kick(SocketUser user, string reason = null)
        {
            if (!await _comand.GetComendConfig(Context.Guild.Id, TypeComand.moderation)) return;
            bool hasPermision = false;
            var permision = (Context.User as SocketGuildUser).Roles.ToList();
            for (int i = 0; i < permision.Count; i++)
            {
                if (permision[i].Permissions.Administrator || await _ModerationRoles.RoleHasPermision(Context.Guild.Id, permision[i].Id, PermisionLevel.Modelators))
                {
                    hasPermision = true;
                    break;
                }
                if (!hasPermision && !(Context.Guild.Owner == Context.User)) return;

                await (user as SocketGuildUser).KickAsync(reason,null);
            }

            await SendMesage(Context, user as SocketGuildUser, "kick", reason, Discord.Color.Orange);
            await Context.Message.DeleteAsync();
        }

        private async Task<bool> TextMutes(SocketGuildUser user)
        {
            if (! await _punishmentRole.GuidHasPunishmentRoleAsync(user.Guild.Id)) await CreateRoles(user.Guild);
            else
            {
                var role = user.Guild.GetRole((await _punishmentRole.GetPanishmentRole(user.Guild.Id)).Vmute);
                if (user.Roles.Contains(role)) return false;
            }
            var Role = await _punishmentRole.GetPanishmentRole(user.Guild.Id, PRoleType.Tmute);
            await user.AddRoleAsync(Role, null);
            return true;
        }
        private async Task<bool> VoiceMutes(SocketGuildUser user)
        {
            if (!await _punishmentRole.GuidHasPunishmentRoleAsync(user.Guild.Id)) await CreateRoles(user.Guild);
            else
            {
                var role =  user.Guild.GetRole((await _punishmentRole.GetPanishmentRole(user.Guild.Id)).Vmute);
                if( user.Roles.Contains(role)) return false;
            }
            var Role = await _punishmentRole.GetPanishmentRole(user.Guild.Id, PRoleType.Vmute);
            await user.AddRoleAsync(Role, null);
            return true;
        }
        private async Task CreateRoles(SocketGuild guild, String Vmute = "Vmute", String Tmute = "Tmute")
        {
            var perm = new GuildPermissions(false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, true, false, false, false, false, false, false, false, false, false, false, false);
            var Vmutes = await guild.CreateRoleAsync(Vmute, perm, new Discord.Color(144,144,144),false,null);
            var Tmutes = await guild.CreateRoleAsync(Tmute, perm, new Discord.Color(144,144,144),false,null);
            var Vpermision = new Discord.OverwritePermissions(0, 3146240);
            var Tpermision = new Discord.OverwritePermissions(0, 2048);
            foreach (var i in guild.Channels.ToList())
            {

                await i.AddPermissionOverwriteAsync(Vmutes, Vpermision);
                await i.AddPermissionOverwriteAsync(Tmutes, Tpermision);
            }
            await _punishmentRole.AddPunishRole(guild.Id, Tmutes.Id, Vmutes.Id);

        }
        private async Task CreateRoles(UInt64 GuidId, String Vmute = "Vmute", String Tmute = "Tmute")
        {

             await CreateRoles(_client.GetGuild(GuidId), Vmute, Tmute);
        }
        private async Task<IMessage> SendMesage(SocketCommandContext _conetext, SocketGuildUser user, string type, string time = null, string reason = null, Color? color = null)
        {
            var guid = _conetext.Guild ?? user.Guild;
            var colors = color ?? Discord.Color.Blue;
            var lang = (await _language.GetLanguage(guid.Id)).GetSection("Moderation").GetSection(type);
            var embed = new EmbedBuilder()
                 .WithAuthor(user)
                 .WithTitle(lang["Title"])
                 .WithDescription(lang["Description"].Replace("{mention}", user.Mention))
                 .AddField(lang["Time"], time ?? "-", true)
                 .AddField("Modelator", _conetext.User.Mention, true)
                 .AddField(lang["Reson"], reason ?? "-", true)
                 .WithThumbnailUrl(user.GetAvatarUrl())
                 .WithFooter(_client.CurrentUser.Username + "#" + _client.CurrentUser.Discriminator, _client.CurrentUser.GetAvatarUrl())
                 .WithColor(colors)
                 .Build();
            return await _conetext.Channel.SendMessageAsync(embed: embed);
        }
        private async Task<IMessage> SendMesage(
            SocketCommandContext _conetext, SocketGuildUser user, string type, string reason = null, Color? color = null)
        {
            var guid = _conetext.Guild ?? user.Guild;
            var colors = color ?? Discord.Color.Blue;

            var lang = (await _language.GetLanguage(guid.Id)).GetSection("Moderation").GetSection("type");
            var embed = new EmbedBuilder()
                 .WithAuthor(user)
                 .WithTitle(lang["Title"])
                 .WithDescription(lang["Description"].Replace("{mention}", user.Mention))
                 .AddField("Modelator", _conetext.User.Mention, true)
                 .AddField(lang["Reson"], reason ?? "-", true)
                 .WithThumbnailUrl(user.GetAvatarUrl())
                 .WithFooter(_client.CurrentUser.Username + "#" + _client.CurrentUser.Discriminator, _client.CurrentUser.GetAvatarUrl())
                 .WithColor(colors)
                 .Build();
            return await _conetext.Channel.SendMessageAsync(embed: embed);
        }
        private async Task<IMessage> SendMesageError(SocketCommandContext _conetext, string type, Color? color = null)
        {
            var guid = _conetext.Guild;
            var colors = color ?? Discord.Color.Red;
            var lang = (await _language.GetLanguage(guid.Id)).GetSection("Moderation").GetSection(type);
            var embed = new EmbedBuilder()
                 .WithTitle(lang["Error"])                
                 .WithFooter(_client.CurrentUser.Username + "#" + _client.CurrentUser.Discriminator, _client.CurrentUser.GetAvatarUrl())
                 .WithColor(colors)
                 .Build();
            return await _conetext.Channel.SendMessageAsync(embed: embed);
        }
    }
}
