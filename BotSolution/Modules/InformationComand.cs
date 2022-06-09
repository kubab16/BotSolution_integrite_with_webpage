using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Infrastructure;
using Infrastructure.Languages;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BotProjectSolution.Comands
{
    public class InformationComand : ModuleBase<SocketCommandContext>
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _service;
        private readonly IConfiguration _configuration;
        private readonly Servers _servers;
        private readonly Languages _language;

        public InformationComand(IServiceProvider provider, DiscordSocketClient client, CommandService service, IConfiguration configuration, Servers servers, Languages languages)
        {
            _provider = provider;
            _client = client;
            _service = service;
            _configuration = configuration;
            _servers = servers;
            _language = languages;
        }

        [Command("ping")]
        public async Task ping()
        {
            var FistTime = Context.Message.Timestamp;
            var TimeReaction = DateTime.Now.Millisecond;
            var message = await Context.Channel.SendMessageAsync("Pong!");
            var SecondTime = message.Timestamp;
            var guid = Context.Guild;
            var lang = (await _language.GetLanguage(guid.Id)).GetSection("Information").GetSection("Ping");
            var MilisecondMessage = FistTime.Millisecond - SecondTime.Millisecond;
            var MilisecondAPI = TimeReaction - FistTime.Millisecond;
            if (MilisecondMessage < 0) MilisecondMessage *= -1;
            if (MilisecondAPI < 0) MilisecondAPI *= -1;

            var embed = new EmbedBuilder()
                .WithAuthor(Context.Client.CurrentUser)
                .WithTitle(lang["Title"])
                .WithDescription($"{lang["API"]} {MilisecondAPI}ms.\n {lang["BOT"]} {MilisecondMessage}ms.")
                .WithFooter(Context.Guild.Name, Context.Guild.IconUrl)
                .WithColor(new Color(255, 249, 126))
                .Build();
            await message.ModifyAsync(msg =>
            {
                msg.Embed = embed;
                msg.Content = "";
            });
        }

        [Command("user")]
        [Alias("user", "whois")]
        public async Task Info(SocketUser users = null)
        {
            var guid = Context.Guild;
            var lang = (await _language.GetLanguage(guid.Id)).GetSection("Information").GetSection("User");
            SocketUser user = users ?? Context.User;
            var bot = "";
            if (user.IsBot)
            {
                bot = lang["Bot"];
            }
            else
            {
                bot = lang["NotBot"];
            }
            var builder = new EmbedBuilder()
                .WithAuthor(Context.Client.CurrentUser.Username, Context.Client.CurrentUser.GetAvatarUrl() ?? Context.Client.CurrentUser.GetDefaultAvatarUrl())
                .WithThumbnailUrl(user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl())
                .WithDescription($"{lang["Title"]} {user.Mention}")
                .AddField(lang["CreationAccountDate"], $"`{ user.CreatedAt.ToString("G")} `", true)
                .AddField(lang["JoinDate"], $"`{ (user as SocketGuildUser).JoinedAt.Value.ToString("G")}`", true)
                .AddField($"Bot: ", bot, false)
                .AddField($"Role:", $"{string.Join(" ", (user as SocketGuildUser).Roles.Select(x => x.Mention))}", false)
                .WithColor(new Color(0, 255, 0))
                .WithFooter($"ID: { user.Id.ToString()}", Context.Guild.IconUrl);
            Embed embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
        }

        [Command("server")]
        [RequireUserPermission(GuildPermission.ManageChannels)]
        public async Task server()
        {
            var guid = Context.Guild;
            var lang = (await _language.GetLanguage(guid.Id)).GetSection("Information").GetSection("Server");
            var builder = new EmbedBuilder()
                .WithAuthor(Context.Client.CurrentUser.Username, Context.Client.CurrentUser.GetAvatarUrl() ?? Context.Client.CurrentUser.GetDefaultAvatarUrl())
                .WithColor(new Color(0, 255, 0))
                .WithThumbnailUrl(Context.Guild.IconUrl)
                .WithDescription($"{lang["Name"]} {Context.Guild.Name}")
                .AddField(lang["Owner"], Context.Guild.Owner.Mention, false)
                .AddField($"{lang["Roles"]} {Context.Guild.Roles.Count}: ", string.Join(" ", Context.Guild.Roles.Select(x => x.Mention)), true)
                .AddField(lang["CountPoople"], Context.Guild.Users.Count, true)
                .WithFooter(Context.Guild.Name, Context.Guild.IconUrl);
            Embed embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
        }

        [Command("avatar")]
        [Alias("user", "whois")]
        public async Task avatar(SocketUser users = null)
        {
            SocketUser user = users ?? Context.User;
            var builder = new EmbedBuilder()
                .WithAuthor(Context.Client.CurrentUser.Username, Context.Client.CurrentUser.GetAvatarUrl() ?? Context.Client.CurrentUser.GetDefaultAvatarUrl())
                .WithColor(new Color(0, 255, 0))
                .AddField($"Avatar: ", $"{user.Mention}", false)
                .WithImageUrl(user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl())
                .WithFooter(Context.Guild.Name, Context.Guild.IconUrl);
            Embed embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
        }

        [Command("help")]
        public async Task help()
        {
            var guid = Context.Guild;
            var lang = (await _language.GetLanguage(guid.Id)).GetSection("Information").GetSection("Server");
            var builder = new EmbedBuilder()
                .WithAuthor(Context.Client.CurrentUser.Username, Context.Client.CurrentUser.GetAvatarUrl() ?? Context.Client.CurrentUser.GetDefaultAvatarUrl())
                .WithTitle(lang["Title"])
                .AddField(lang["Information"], lang["InformationList"], false)
                .AddField(lang["Configuration"], lang["ConfigurationList"], false)
                .AddField(lang["Modration"], lang["ModerationList"], false)
                .WithFooter(Context.Guild.Name, Context.Guild.IconUrl);
            Embed embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
        }

    }
}
