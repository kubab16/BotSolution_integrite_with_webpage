using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BotSolution.Services
{
    [Obsolete]
    public class CommandHandler : DiscordClientService
    {
        private readonly IServiceProvider provider;
        private readonly DiscordSocketClient client;
        private readonly CommandService service;
        private readonly IConfiguration configuration;
        private readonly Servers servers;

        public CommandHandler(IServiceProvider provider, 
            DiscordSocketClient client, CommandService service, 
            IConfiguration configuration, Servers servers, ILogger<CommandHandler> logger) : base(client, logger)
        {
            this.provider = provider;
            this.client = client;
            this.service = service;
            this.configuration = configuration;
            this.servers = servers;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.client.MessageReceived += OnMessageReceived;
            this.service.CommandExecuted += OnCommandExecuted;
            await this.service.AddModulesAsync(Assembly.GetEntryAssembly(), this.provider);
        }

        private async Task OnCommandExecuted(Optional<CommandInfo> commandInfo, ICommandContext commandContext, IResult result)
        {
            if (result.IsSuccess)
            {
                return;
            }

            await commandContext.Channel.SendMessageAsync(result.ErrorReason);
        }

        private async Task OnMessageReceived(SocketMessage socketMessage)
        {
            if (!(socketMessage is SocketUserMessage message)) return;
            if (message.Source != MessageSource.User) return;

            var argPos = 0;

            var prefix = await servers.GetGuildPrefix((message.Channel as SocketGuildChannel).Guild.Id) ?? this.configuration["Prefix"];
            if (!message.HasStringPrefix(prefix, ref argPos) && !message.HasStringPrefix(client.CurrentUser.Mention, ref argPos) && !message.HasMentionPrefix(this.client.CurrentUser, ref argPos)) return;
            
            var context = new SocketCommandContext(this.client, message);
            await this.service.ExecuteAsync(context, argPos, this.provider);
        }
    }
}
