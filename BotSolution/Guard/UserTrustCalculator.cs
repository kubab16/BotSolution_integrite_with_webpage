using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace BotSolution.Guard
{
    public static class UserTrustCalculator
    {
        
        public static async Task<ulong> DateJoinUserTrust(SocketGuildUser user)
        {
            var time = (user.JoinedAt.Value.Millisecond - user.CreatedAt.Millisecond) / 86400000;
            ulong trustValue = 0;

            if (time >= 7)
            {
                trustValue += 3;
            }
            
            if (time >= 30)
            {
                trustValue += 5;
            }

            if (time >= 180)
            {
                trustValue += 10;
            }

            if (time >= 360)
            {
                trustValue += 30;
            }
            
            return (ulong) await Task.FromResult(trustValue);
        }

        public static async Task<bool> UserAvatarTrust(SocketGuildUser user)
        {
            return user.GetAvatarUrl() != user.GetDefaultAvatarUrl();
        }

        public static async Task<ulong> UserAvatarTrustInt(SocketGuildUser user)
        {
            if (user.GetAvatarUrl() != user.GetDefaultAvatarUrl())
            {
                return 12;
            }
            else
            {
                return 0;
            }
        }
        
        public static async Task<ulong> UserRoleTrust(SocketGuildUser user)
        {
            return (ulong) await Task.FromResult(user.Roles.Count / 2 - 1);
        }
    }
}