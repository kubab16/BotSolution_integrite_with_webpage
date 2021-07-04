using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ModerationRoles
    {
        private readonly Context _context;
        public ModerationRoles(Context context)
        {
            _context = context;
        }
        public async Task<ModerationRole> RolePermision(UInt64 GuidId, UInt64 RoleId)
        {
            var permision = await _context.ModerationRoles
                .FindAsync(GuidId, RoleId);
            return await Task.FromResult(permision);
        }
        public async Task AddUserPermision(UInt64 GuidId, UInt64 RoleId, string permision)
        {          
            var Permisions = await _context.ModerationRoles
                .FindAsync(GuidId, RoleId);
            if (Permisions == null)
            {
                var NewPermisionRole = new ModerationRole { GuildID = GuidId, RoleId = RoleId };
                NewPermisionRole.SetSection(permision, true);
                _context.ModerationRoles.Add(NewPermisionRole);
            }
            else
            {
                Permisions.SetSection(permision, true);
            }
            _context.SaveChanges();
        }
        public async Task RemoveUserPermision(UInt64 GuidId, UInt64 RoleId, string permision)
        {
            var Permisions = await _context.ModerationRoles
                .FindAsync(GuidId, RoleId);
            if (Permisions == null)
            {
                var NewPermisionRole = new ModerationRole { GuildID = GuidId, RoleId = RoleId };
                NewPermisionRole.SetSection(permision, false);
                _context.ModerationRoles.Add(NewPermisionRole);
            }
            else
            {
                Permisions.SetSection(permision, false);
            }
            _context.SaveChanges();
        }
    }
}

