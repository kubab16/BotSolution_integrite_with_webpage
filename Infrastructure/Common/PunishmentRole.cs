using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public enum PRoleType
    {
        Tmute,
        Vmute
    }
    public class PunishmentRole
    {
        private readonly Context _context;
        public PunishmentRole(Context context)
        {
            _context = context;
        }
        public async Task<bool> GuidHasPunishmentRoleAsync(UInt64 GuidId)
        {
            var GuidPerm = await _context.Servers.Where(x => x.Id == GuidId).FirstOrDefaultAsync();
            if (GuidPerm != null) return true; else return false;
        }
        public async Task<ulong> GetPanishmentRole(UInt64 GuidId, PRoleType roleType)
        {
            var pRole = await _context.Servers
                .Where(x => x.Id == GuidId)
                .FirstOrDefaultAsync();
            if (pRole == null) return 0;
            switch(roleType)
            {
                case PRoleType.Tmute:
                    return await Task.FromResult(pRole.Tmute);
                case PRoleType.Vmute:
                    return await Task.FromResult(pRole.Vmute);
                default:
                    return 0;
            }
        }
        public async Task<Server> GetPanishmentRole(UInt64 GuidId)
        {
            var pRole = await _context.Servers
                .FindAsync(GuidId);
            return await Task.FromResult(pRole);
            
        }
        public async Task AddPunishRole(UInt64 GuidId, UInt64 TMute, UInt64 VMute)
        {
            var PunishRole = await _context.Servers.FindAsync(GuidId);
            if (PunishRole == null)
                _context.Servers.Add(new Server {Id = GuidId, Vmute = VMute,Tmute = TMute });
            else
            {
                PunishRole.Vmute= VMute;
                PunishRole.Tmute = TMute;
            }

            await _context.SaveChangesAsync();
        }
    }
}
