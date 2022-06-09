using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace BotSolution.Bot.Modules
{
    public enum TypeOfMessage
    {
        Join,
        Left,
        Ban
    }
    public static class GuidMessages
    {
        public static Task SendMessage([NotNullAttribute] SocketUser user, [NotNullAttribute] TypeOfMessage message)
        {
            string name = user.Username.ToString();
            string discrominator = user.Discriminator.ToString();
            string guidName = (user as SocketGuildUser).Guild.Name.ToString();
            int NumberOfPeople = (user as SocketGuildUser).Guild.Users.Count;
            SocketGuild guild = (user as SocketGuildUser).Guild;
            switch (message)
            {
                case TypeOfMessage.Join:
                    var embed = new EmbedBuilder()
                        .WithAuthor(name + discrominator, user.GetAvatarUrl())
                        .WithTitle($"Witamy na naszym serwerze {name}!")
                        .WithDescription($"Witamy na naszym serwerze {user.Mention}, zapoznaj się z reguraminem i go zaakceptuj aby uzyskać dostęp do reszty kanałów")
                        .WithThumbnailUrl(user.GetAvatarUrl())
                        .WithFooter(guild.Name.ToString(), guild.IconUrl)
                        .Build();
                    break;
                case TypeOfMessage.Left:
                    break;
                case TypeOfMessage.Ban:
                    break;
                default:
                    break;

            }
            return Task.CompletedTask;
        }
    }
}
