using BotSolution.Bot.Modules;
using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BotSolution.Bot.Service
{
    [Obsolete]
    class EventsHandler : InitializedService
    {
        private readonly IServiceProvider provider;
        private readonly DiscordSocketClient client;
        private readonly IConfiguration configuration;
        private readonly CommandService service;

        public EventsHandler(IServiceProvider provider, DiscordSocketClient client, CommandService service, IConfiguration configuration)
        {
            this.provider = provider;
            this.client = client;
            this.service = service;
            this.configuration = configuration;
        }

        public override async Task InitializeAsync(CancellationToken cancellationToken)
        {
            this.client.UserJoined += OnUserJoined;
            this.client.UserLeft += OnUserLeft;
            this.client.UserBanned += OnUserBanned;
            this.client.UserUnbanned += OnUserUnbanned;
            this.client.UserUpdated += OnUserUpdated;
            this.client.MessageDeleted += OnMessageDeleted;
            this.client.MessageReceived += OnMessageReceived;
            this.client.MessageUpdated += OnMessageUpdated;
            this.client.ChannelCreated += OnChannelCreated;
            this.client.ChannelUpdated += OnChannelUpdated;
            this.client.ChannelDestroyed += OnChannelDestroyed;
            await this.service.AddModulesAsync(Assembly.GetEntryAssembly(), this.provider);
        }

        private async Task OnUserJoined(SocketGuildUser user)
        {
            GuidMessages.SendMessage(user,TypeOfMessage.Join);
        }

        private async Task OnUserLeft(SocketGuildUser user)
        {
            GuidMessages.SendMessage(user, TypeOfMessage.Left);
        }

        private async Task OnUserBanned(SocketUser user, SocketGuild guild)
        {
            ;
        }

        private async Task OnUserUnbanned(SocketUser user, SocketGuild guild)
        {
            ;
        }

        private async Task OnUserUpdated(SocketUser user, SocketUser user1)
        {
            ;
        }

        private async Task OnMessageDeleted(Cacheable<IMessage, ulong> cacheable, ISocketMessageChannel channel)
        {
            ;
        }

        private async Task OnMessageReceived(SocketMessage message)
        {
            ;
        }

        private async Task OnMessageUpdated(Cacheable<IMessage, ulong> cacheable, SocketMessage message, ISocketMessageChannel channel)
        {
            ;
        }

        private async Task OnChannelCreated(SocketChannel channel)
        {
            ;
        }

        private async Task OnChannelUpdated(SocketChannel PreviuceChannel,SocketChannel NowChannel)
        {
            ;
        }

        private async Task OnChannelDestroyed(SocketChannel channel)
        {
            ;
        }
    }
}
