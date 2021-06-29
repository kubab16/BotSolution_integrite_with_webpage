using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BotSolution.Service
{
    [Obsolete]
    public class PunishmentHandler : InitializedService
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _client;
        private readonly Punishments _punishment;
        private readonly PunishmentRole _punishmentRole;
        private readonly CommandService _service;

        public PunishmentHandler(IServiceProvider provider, DiscordSocketClient client, Punishments punishments, PunishmentRole punishmentRole, CommandService service)
        {
            _provider = provider;
            _client = client;
            _punishment = punishments;
            _punishmentRole = punishmentRole;
            _service = service;
        }

        public override async Task InitializeAsync(CancellationToken cancellationToken)
        {
            var NewTask = new Task(async () => await StartMuteHandler());
            NewTask.Start();
            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);                   
        }
        private async Task StartMuteHandler()
        {
            while (true)
            {
                await MuteHandler();
                await Task.Delay(1 * 60 * 1000);               
            }
        }
        private async Task MuteHandler()
        {
            List<Punishment> punishment = await _punishment.GetAllActivePunishments();
            foreach(var panish in punishment)
            {
                if(panish.EndDate >= DateTime.Now && panish.EndDate != null)
                {
                    var guid = _client.GetGuild(panish.GuidId);
                    var user = guid.GetUser(panish.UserId);
                    if( await _punishmentRole.GuidHasPunishmentRoleAsync(guid.Id));
                    switch (panish.TypeOfPunishment)
                    {
                        case typePunishment.textmute:
                            
                            var role = guid.GetRole(await _punishmentRole.GetPanishmentRole(guid.Id, PRoleType.Tmute));
                            await user.RemoveRoleAsync(role);
                            break;
                        case typePunishment.vmute:
                            role = guid.GetRole(await _punishmentRole.GetPanishmentRole(guid.Id, PRoleType.Vmute));
                            await user.RemoveRoleAsync(role);
                            break;
                        case typePunishment.ban:
                            await guid.RemoveBanAsync(panish.UserId);
                            break;

                    }
                    await _punishment.FinishPunishmenst(panish.id);
                    
                }
            }           
        }
       
    }
    
}
