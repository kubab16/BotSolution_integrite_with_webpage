using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class typePunishment
    {
        public const String
        warn = "warn",
        textmute = "tmute",
        vmute = "vmute",
        ban = "ban";
    }
    public class Punishments
    {
        private readonly MyBotContext _context;
        public Punishments(MyBotContext context)
        {
            _context = context;
        }
        public async Task<List<Punishment>> GetAllActivePunishments()
        {
            var result = await _context.Punishment
                .Where(x => x.Finished == false)
                .Where(x => x.TypeOfPunishment != typePunishment.warn)
                .ToListAsync();
            return await Task.FromResult(result);
        }
        public async Task<List<Punishment>> GetAllActivePunishmentsGuid(ulong GuidId)
        {
            var result = await _context.Punishment
                .Where(x => x.Finished == false)
                .Where(x => x.GuidId == GuidId)
                .ToListAsync();
            return await Task.FromResult(result);
        }
        public async Task<List<Punishment>> GetAllActivePunishmentsUser(ulong UserId,ulong GuidId)
        {
            var result = await _context.Punishment
                .Where(x => x.Finished == false)
                .Where(x => x.GuidId == GuidId)
                .Where(x => x.UserId == UserId)
                .ToListAsync();
            return await Task.FromResult(result);
        }
        public async Task<List<Punishment>> GetAllPunishmentsUser(ulong UserId, ulong GuidId)
        {
            var result = await _context.Punishment
                .Where(x => x.GuidId == GuidId)
                .Where(x => x.UserId == UserId)
                .ToListAsync();
            return await Task.FromResult(result);
        }
        public async Task AddPunishmentUser(ulong GuidId, ulong UserId, String Type,DateTime? EndDate, ulong ModeratorId, String Reason = null)
        {

            _context.Punishment.Add(new Punishment { GuidId = GuidId, UserId = UserId,
                TypeOfPunishment = Type, EndDate = EndDate,
                Reason = Reason, ModeratorId = ModeratorId });
            await _context.SaveChangesAsync();
        }
        public async Task AddPunishmentUser(ulong GuidId, ulong UserId, String Type, ulong ModeratorId, String Reason = null)
        {

            _context.Punishment.Add(new Punishment { GuidId = GuidId, UserId = UserId,
                TypeOfPunishment = Type,
                Reason = Reason, ModeratorId = ModeratorId });
            await _context.SaveChangesAsync();
        }
        public async Task FinishPunishmenst( Int32 PunishId)
        {
            var punish = await _context.Punishment
                .FindAsync(PunishId);
            if(punish != null)
            {
                punish.Finished = true;
            }
            await _context.SaveChangesAsync();
        }
    }
}