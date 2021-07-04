using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AutoRoles
    {
        private readonly Context _context;

        public AutoRoles(Context context)
        {
            _context = context;
        }
        // Dead source to repair 
        public async Task<List<Server>> GetAutoRolesAsync(ulong id)
        {
            var autoRoles = await _context.Servers
                .Where(x => x.Id == id)
                .ToListAsync();

            return await Task.FromResult(autoRoles);
        }

        public async Task AddAutoRoleAsync(ulong id, ulong roleId)
        {
            var server = await _context.Servers
                .FindAsync(id);

            if (server == null)
                _context.Add(new Server { Id = id });

            _context.Add(new Server { AutoRoleId = roleId, Id = id });
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAutoRoleAsync(ulong id)
        {
            var autoRole = await _context.Servers
                .FindAsync(id);
                
            if(autoRole != null)
            {
                autoRole = null;
            }

            await _context.SaveChangesAsync();
        }

        public async Task ClearAutoRolesAsync(List<Server> autoRoles)
        {
            _context.RemoveRange(autoRoles);
            await _context.SaveChangesAsync();
        }
    }
}
