using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common
{
    public class TrustUsers
    {
        private readonly Context _context;
        public TrustUsers(Context context)
        {
            _context = context;
        }

        public async Task<bool> NewUser(ulong guildId, ulong userId, ulong trustValue)
        {
            var user = await _context.TrustUsers
                .FirstOrDefaultAsync(x => x.UserId == userId && x.GuildId == guildId);
            if (user == null)
            {
                var operation = _context.Add(new Infrastructure.TrustUser
                {
                    GuildId = guildId,
                    UserId = userId,
                    TrustValue = trustValue
                });
                await _context.SaveChangesAsync();
                return await Task.FromResult(await _context.TrustUsers
                    .FirstOrDefaultAsync(
                        x => x.UserId == userId && x.GuildId == guildId) != null
                    );
            }
            else
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> ChangeTrustValue(ulong guildId, ulong userId, ulong trustValue)
        {
            var user = await _context.TrustUsers
                .FirstOrDefaultAsync(x => x.UserId == userId && x.GuildId == guildId);
            if (user == null)
            {
                return await Task.FromResult(false);
            }
            else
            {
                user.TrustValue += trustValue;
                await _context.SaveChangesAsync();
                return await Task.FromResult(await _context.TrustUsers
                        .FirstOrDefaultAsync(
                            x => x.UserId == userId && x.GuildId == guildId) != null
                );
            }
            
        }

        public async Task<bool> AddMessaageToCount(ulong guildId, ulong userId)
        {
            var user = await _context.TrustUsers
                .FirstOrDefaultAsync(x => x.UserId == userId && x.GuildId == guildId);
            if (user == null)
            {
                return await Task.FromResult(false);
            }
            else
            {
                user.MessageCount++;
                await _context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
        }
        
        public async Task<bool> AddMessaageToCount(ulong guildId, ulong userId, ulong cout)
        {
            var user = await _context.TrustUsers
                .FirstOrDefaultAsync(x => x.UserId == userId && x.GuildId == guildId);
            if (user == null)
            {
                return await Task.FromResult(false);
            }
            else
            {
                user.MessageCount += cout;
                await _context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
        }

        public async Task<ulong> GetUserTrustValue(ulong guildId, ulong userId, ulong changeValue)
        {
            var user = await _context.TrustUsers
                .FirstOrDefaultAsync(x => x.UserId == userId && x.GuildId == guildId);
            if (user == null)
            {
                return 0;
            }
            else
            {
                var userPunish = await _context.Punishment
                    .Where(x => x.GuidId == guildId && x.UserId == userId)
                    .ToListAsync();
                ulong value = user.MessageCount / 125 + user.TrustValue + changeValue;
                if (userPunish != null)
                {
                    value = value - (ulong) (userPunish.Count / 5) + 2;
                }
                return value;
            }
        }
    }
}