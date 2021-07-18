using BotSolution.Bot.Service;
using BotSolution.Service;
using BotSolution.Services;
using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Infrastructure;
using Infrastructure.Bot;
using Infrastructure.Languages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BotSolution.Bot
{
    public class RunBot
    {
        private static IHost host;
        /// <summary>
        /// Start bot
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public static async Task start()
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration(x =>
                {
                    var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("Config.json", false, true)
                        .Build();

                    x.AddConfiguration(configuration);
                })
                .ConfigureLogging(x =>
                {
                    x.AddConsole();
                    x.SetMinimumLevel(LogLevel.Debug);
                })
                .ConfigureDiscordHost((context, config) =>
                {
                    config.SocketConfig = new DiscordSocketConfig
                    {
                        LogLevel = LogSeverity.Debug,
                        AlwaysDownloadUsers = false,
                        MessageCacheSize = 200,
                    };

                    config.Token = context.Configuration["Token"];
                })
                .UseCommandService((context, config) =>
                {
                    config.CaseSensitiveCommands = false;
                    config.LogLevel = LogSeverity.Debug;
                    config.DefaultRunMode = RunMode.Sync;
                })
                .ConfigureServices((context, services) =>
                {
                    services
                        .AddHostedService<CommandHandler>()
                        .AddHostedService<EventsHandler>()
                        .AddHostedService<PunishmentHandler>()
                        .AddDbContext<Context>()
                        .AddSingleton<Servers>()
                        .AddSingleton<AutoRoles>()
                        .AddSingleton<Comand>()
                        .AddSingleton<Webhook>()
                        .AddSingleton<Punishments>()
                        .AddSingleton<ModerationRoles>()
                        .AddSingleton<PunishmentRole>()
                        .AddSingleton<Languages>();
                })
                .UseConsoleLifetime();

            host = builder.Build();
            using (host)
            {
                await host.RunAsync();
            }
        }
        /// <summary>
        /// Stop bot
        /// </summary>
        /// <returns></returns>
        public static async Task StopBot()
        {
            using (host)
            {
                await host.StopAsync();
            }
        }
    }
}
