using Discord;
using Discord.Commands;
using Discord.Webhook;
using Discord.WebSocket;
using Infrastructure;
using Infrastructure.Bot;
using Infrastructure.Languages;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BotSolution.Modules
{
    public class Configuration : ModuleBase<SocketCommandContext>
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _service;
        private readonly ModerationRoles _ModerationRoles;
        private readonly Servers _servers;
        private readonly Webhook _webhook;
        private readonly Comand _comand;
        private readonly Languages _language;

        public Configuration(IServiceProvider provider, DiscordSocketClient client, CommandService service,
                             IConfiguration configuration, Servers servers, Webhook webhook, Comand comand,
                             Languages languages)
        {
            _client = client;
            _service = service;
            _servers = servers;
            _webhook = webhook;
            _comand = comand;
            _language = languages;
        }
        [Command("setprefix")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]
        public async Task setprefix(string prefix)
        {
            bool hasPermision = false;
            var permision = (Context.User as SocketGuildUser).Roles.ToList();
            foreach (var perm in permision)
            {
                if (perm.Permissions.Administrator
                    || (await _ModerationRoles.RolePermision(Context.Guild.Id, perm.Id)).Configuration
                    )
                {
                    hasPermision = true;
                    break;
                }
            }
            if (hasPermision)
                return;
            string oldprefix = await _servers.GetGuildPrefix(Context.Guild.Id);
            await _servers.ModifyGuildPrefix(Context.Guild.Id, prefix);

            var guid = Context.Guild;
            var lang = (await _language.GetLanguage(guid.Id)).GetSection("Configuration").GetSection("Setprefix");
            var embed = new EmbedBuilder()
                .WithAuthor(Context.Client.CurrentUser)
                .WithTitle(lang["Title"])
                .WithDescription($"{lang["OldPrefix"]} {oldprefix}\n" +
                $" {lang["NewPRefix"]} {prefix}")
                .WithFooter(Context.Guild.Name, Context.Guild.IconUrl)
                .WithColor(new Color(237, 253, 17))
                .Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
        }

        [Command("newwebhook")]
        [Alias("nwebhook", "addwebhook")]
        [RequireUserPermission(GuildPermission.ManageWebhooks)]
        public async Task NewWebhook(SocketChannel channel, string name)
        {
            bool hasPermision = false;
            var permision = (Context.User as SocketGuildUser).Roles.ToList();
            if (Context.Guild.Owner.Id != Context.User.Id)
            {
                foreach (var perm in permision)
                {
                    if (perm.Permissions.Administrator
                        || (await _ModerationRoles.RolePermision(Context.Guild.Id, perm.Id)).Configuration
                        )
                    {
                        hasPermision = true;
                        break;
                    }
                }
            }
            else
                hasPermision = true;
            if (!hasPermision)
                return;
            var NewWebhook = await (channel as SocketTextChannel).CreateWebhookAsync(name);
            var guid = Context.Guild;
            var lang = (await _language.GetLanguage(guid.Id)).GetSection("Configuration").GetSection("NewWebhooks");
            _webhook.AddWebhook(guid.Id, NewWebhook.Token, NewWebhook.Id, NewWebhook.Name);
            var embed = new EmbedBuilder()
                .WithAuthor(Context.Client.CurrentUser)
                .WithTitle(lang["Title"])
                .WithDescription($"Webkook: {NewWebhook.Name} \n" +
                $" {lang["Channel"]} {(guid as SocketGuild).GetChannel(NewWebhook.ChannelId).Name}\n " +
                $"{lang["Creator"]} {Context.User.Username}#{Context.User.Discriminator}")
                .WithFooter(guid.Name, Context.Guild.IconUrl)
                .WithColor(new Color(237, 253, 17))
                .Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
        }

        [Command("deletewebhook")]
        [Alias("rwebhook", "remowewebhook")]
        [RequireUserPermission(GuildPermission.ManageWebhooks)]
        public async Task DeleteWebhook(SocketChannel channel, string name)
        {
            bool hasPermision = false;
            var permision = (Context.User as SocketGuildUser).Roles.ToList();
            if (Context.Guild.Owner.Id != Context.User.Id)
            {
                foreach (var perm in permision)
                {
                    if (perm.Permissions.Administrator
                        || (await _ModerationRoles.RolePermision(Context.Guild.Id, perm.Id)).Configuration
                        )
                    {
                        hasPermision = true;
                        break;
                    }
                }
            }
            else
                hasPermision = true;
            if (!hasPermision)
                return;
            Webhooks webhooks = await _webhook.GetWebhook(Context.Guild.Id, name);
            var Webhook = new DiscordWebhookClient(webhooks.id, webhooks.Token);

            await _webhook.RemoveWebhook(Context.Guild.Id, webhooks.Token);

            var guid = Context.Guild;
            var lang = (await _language.GetLanguage(guid.Id)).GetSection("Configuration").GetSection("Setprefix");
            var embed = new EmbedBuilder()
                .WithAuthor(Context.Client.CurrentUser)
                .WithTitle(lang["Title"])
                .WithFooter(Context.Guild.Name, Context.Guild.IconUrl)
                .WithColor(new Color(237, 253, 17))
                .Build();
            await Webhook.DeleteWebhookAsync();
            await Context.Channel.SendMessageAsync(null, false, embed);
        }

        [Command("comand")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [Alias("enable", "disable")]
        public async Task comand(string comand, string ED)
        {
            bool hasPermision = false;
            var permision = (Context.User as SocketGuildUser).Roles.ToList();
            foreach (var perm in permision)
            {
                if (perm.Permissions.Administrator
                    || (await _ModerationRoles.RolePermision(Context.Guild.Id, perm.Id)).Configuration
                    )
                {
                    hasPermision = true;
                    break;
                }
            }
            if (hasPermision)
                return;
            switch (ED)
            {
                case "enable":
                case "e":
                case "true":
                case "t":
                    switch (comand)
                    {
                        case "info":
                        case "information":
                            await _comand.EnableComand(Context.Guild.Id, TypeComand.Inoformation);
                            break;
                        case "mod":
                        case "moderation":
                            await _comand.EnableComand(Context.Guild.Id, TypeComand.Inoformation);
                            break;
                        case "miusic":
                        case "sound":
                            await _comand.EnableComand(Context.Guild.Id, TypeComand.miusic);
                            break;
                        case "funny":
                            await _comand.EnableComand(Context.Guild.Id, TypeComand.miusic);
                            break;
                    }
                    break;
                case "disable":
                case "d":
                case "false":
                case "f":
                    switch (comand)
                    {
                        case "info":
                        case "information":
                            await _comand.DiscambleComand(Context.Guild.Id, TypeComand.Inoformation);
                            break;
                        case "mod":
                        case "moderation":
                            await _comand.DiscambleComand(Context.Guild.Id, TypeComand.Inoformation);
                            break;
                        case "miusic":
                        case "sound":
                            await _comand.DiscambleComand(Context.Guild.Id, TypeComand.miusic);
                            break;
                        case "funny":
                            await _comand.DiscambleComand(Context.Guild.Id, TypeComand.miusic);
                            break;
                    }
                    break;
            }
            var configurationComand = await _comand.GetComendsConfig(Context.Guild.Id);

            var guid = Context.Guild;
            var lang = (await _language.GetLanguage(guid.Id)).GetSection("Configuration").GetSection("EnableDisableCommands");
            string info, mod, miusic, funny;
            if (configurationComand.Information) info = lang["TurnOn"]; else info = lang["TurnOff"];
            if (configurationComand.Moderation) mod = lang["TurnOn"]; else mod = lang["TurnOff"];
            if (configurationComand.Miusic) miusic = lang["TurnOn"]; else miusic = lang["TurnOff"];
            if (configurationComand.Funny) funny = lang["TurnOn"]; else funny = lang["TurnOff"];

            var embed = new EmbedBuilder()
                .WithAuthor(_client.CurrentUser)
                .WithTitle(lang["Title"])
                .WithDescription(
                $"{lang["Info"]}        {info}.\n" +
                $"{lang["Moderation"]}  {mod}.\n" +
                $"{lang["Miusic"]}      {miusic}.\n" +
                $"{lang["Funny"]}       {funny}.")
                .WithFooter(Context.Guild.Name, Context.Guild.IconUrl)
                .Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
        }
    }
}
