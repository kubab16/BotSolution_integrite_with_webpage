using BotSolution.Bot.Modules;
using BotSolution.Services;
using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace BotSolution.Bot.Service
{
    [Obsolete]
    class EventsHandler : DiscordClientService
    {
        private readonly IServiceProvider provider;
        private readonly DiscordSocketClient client;
        private readonly CommandService service; 

        public EventsHandler(IServiceProvider provider, DiscordSocketClient client, CommandService service, ILogger<EventsHandler> logger) : base(client, logger)
        {
            this.provider = provider;
            this.client = client;
            this.service = service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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



        private async Task OnUserLeft(SocketGuild guild, SocketUser user)
        {
            await GuidMessages.SendMessage(user, TypeOfMessage.Left);
        }

        private async Task OnUserJoined(SocketGuildUser user)
        {
            GuidMessages.SendMessage(user, TypeOfMessage.Join);
        }


        private async Task OnUserBanned(SocketUser user, SocketGuild guild)
        {
            throw new NotImplementedException();
        }

        private async Task OnUserUnbanned(SocketUser user, SocketGuild guild)
        {
            throw new NotImplementedException();
        }

        private async Task OnUserUpdated(SocketUser user, SocketUser user1)
        {
            throw new NotImplementedException();
        }


        private async Task OnMessageReceived(SocketMessage message)
        {
            throw new NotImplementedException();
        }

        private async Task OnMessageUpdated(Cacheable<IMessage, ulong> cacheable, SocketMessage message, ISocketMessageChannel channel)
        {
            throw new NotImplementedException();
        }

        private Task OnMessageDeleted(Cacheable<IMessage, ulong> arg1, Cacheable<IMessageChannel, ulong> arg2)
        {
            throw new NotImplementedException();
        }

        private async Task OnChannelCreated(SocketChannel channel)
        {
            throw new NotImplementedException();
        }

        private async Task OnChannelUpdated(SocketChannel PreviuceChannel, SocketChannel NowChannel)
        {
            throw new NotImplementedException();
        }

        private async Task OnChannelDestroyed(SocketChannel channel)
        {
            throw new NotImplementedException();
        }
    }
}
