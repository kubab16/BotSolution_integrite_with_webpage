using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Addons.Hosting;
using Discord.Addons.Hosting.Util;
using Discord.Commands;
using Discord.WebSocket;
using Infrastructure;
using Infrastructure.Common;
using Microsoft.Extensions.Logging;

namespace BotSolution.Guard
{
    [Obsolete]
    public class MainGuard : DiscordClientService
    {
        private readonly Servers _servers;
        private readonly TrustUsers _trustUser;
        private readonly CommandService _service;
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _client;
        private readonly BadWords _badWords;
        private readonly PunishmentRole _punishmentRole;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Client.WaitForReadyAsync(stoppingToken);
            _client.MessageReceived += OnMessageDetector;
            _client.UserJoined += OnNewUserJoin;
            _client.ReactionAdded += OnReactionAddPointer;
            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }

        public MainGuard(Servers servers,
            TrustUsers trustUser, CommandService service,
            IServiceProvider provider, DiscordSocketClient client,
            BadWords badWords, PunishmentRole punishmentRole, ILogger<MainGuard> logger) : base(client, logger)
        {
            _servers = servers;
            _trustUser = trustUser;
            _service = service;
            _provider = provider;
            _client = client;
            _badWords = badWords;
            _punishmentRole = punishmentRole;
        }
        /// <summary>
        /// Detect message. If message is odd 
        /// </summary>
        /// <param name="message">Message to scan</param>
        /// <returns></returns>
        private async Task OnMessageDetector(SocketMessage message)
        {
            if (!(message is SocketUserMessage socketUserMessage)) return;
            if (!message.Author.IsBot)
            {
                if (await CheckMessage(message))
                {
                    await _trustUser.AddOddMessaageToCount((message.Author as SocketGuildUser).Guild.Id ,message.Author.Id);
                    await OddMessageReaction(message.Author as SocketGuildUser, message);
                }
                else
                {
                    await _trustUser.AddMessaageToCount((message.Author as SocketGuildUser).Guild.Id ,message.Author.Id);
                }
            }
        }
        
        private async Task OnNewUserJoin(SocketGuildUser user)
        {
            var trustValue = (await UserTrustCalculator.DateJoinUserTrust(user)) +
                                (await UserTrustCalculator.UserAvatarTrustInt(user));
            await _trustUser.NewUser(user.Guild.Id, user.Id, trustValue);
        }
        private async Task OnReactionAddPointer(Cacheable<IUserMessage, ulong> Message, Cacheable<IMessageChannel, ulong> chanel, SocketReaction reaction)
        {
            var idGuild = (reaction.Channel as SocketGuildChannel).Guild.Id;
            var idUser = reaction.User.Value.Id;

            var x = await _trustUser.AddReactionToCount(idGuild, idUser);
        }
        private async Task<bool> CheckMessage(SocketMessage message)
        {
            var listOfBadWords = await _badWords.GetBadWordListAsync();
            bool goodMessage = true;
            foreach (var badWord in listOfBadWords)
            {
                if (message.ToString().Contains(badWord))
                {
                    goodMessage = false;
                    break;
                }
            }

            return goodMessage;
        }
        
        private async Task OddMessageReaction(SocketGuildUser user, SocketMessage message)
        {
            var m = await message.Channel.SendMessageAsync("Uwaga możliwy scam reakcja w toku!!");
            var TrustUser = await _trustUser.GetTrustUser(user.Guild.Id, user.Id );
            if (DateTimeOffset.Now.Millisecond - user.JoinedAt.Value.Millisecond < 108000000 && TrustUser.MessageCount < 100)
            {
                user.BanAsync(7,"Wiadomości phising!!");
                message.DeleteAsync();
                m.DeleteAsync();
                return;
            }

            ulong reactionValue = TrustUser.OddMessageCount/3 + 5;
            if (message.MentionedEveryone)
            {
                reactionValue += 100;
            }
            var trustValue = await UserTrustCalculator.DateJoinUserTrust(user) + await UserTrustCalculator.UserAvatarTrustInt(user) + await UserTrustCalculator.UserRoleTrust(user);
            
            reactionValue -= await _trustUser.GetUserTrustValue(user.Guild.Id, user.Id, trustValue);

            if (reactionValue < 40)
            {
                user.BanAsync( 7,"Wiadomości phising!!");
                message.DeleteAsync();
                m.DeleteAsync();
                return;
            }
            else if (reactionValue < 100)
            {
                user.KickAsync("Wiadomości phising!!");
                message.DeleteAsync();
                m.DeleteAsync();
                return;
            }
            else
            {
                TextMutes(user);
                message.DeleteAsync();
                m.DeleteAsync();
                return;
            }
        }
        
        private async Task<bool> TextMutes(SocketGuildUser user)
        {
            if (! await _punishmentRole.GuidHasPunishmentRoleAsync(user.Guild.Id)) user.KickAsync("Wiadomości phising!!");
            else
            {
                var role = user.Guild.GetRole((await _punishmentRole.GetPanishmentRole(user.Guild.Id)).Vmute);
                if (user.Roles.Contains(role)) return false;
            }
            var Role = await _punishmentRole.GetPanishmentRole(user.Guild.Id, PRoleType.Tmute);
            await user.AddRoleAsync(Role, null);
            return true;
        }
    }
}