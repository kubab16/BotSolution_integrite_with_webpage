using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public enum PermisionLevel
    {
        None = 0,
        Standard = 1,
        MiusicMenagers = 2,
        Modelators = 4,
        ModelatorOlders = 8,
        WebhookSents = 16,
        WebhookMenagers = 32,
        Admins = 64,
        GuidOwners = 128,
        BotDevelopers = 256,
        BotAdmins = 512,
        BotOwners = 1024,
        BlogEditor = 2048,
        BlogAdmin = 4096,
        BlogModelator = 8192,
        WebModelator = 16384,
        WebAdmin = 32768
    }
    public class ModerationRoles
    {
        private readonly MyBotContext _context;
        public ModerationRoles(MyBotContext context)
        {
            _context = context;
        }
        public async Task<bool> RoleHasPermision(UInt64 GuidId, UInt64 RoleId, PermisionLevel permision)
        {
            var PermisionNumber = await _context.ModerationRoles               
                .Where(x => x.id == GuidId)
                .Where(x => x.RoleId == RoleId)
                .FirstOrDefaultAsync();
            if (PermisionNumber == null) return false;
            Int32 permLvl = PermisionNumber.PermisionLevel;
            while (permLvl > 0)
            {
                int higherPerm = Convert.ToInt32(Math.Log2(permLvl));
                if(Math.Pow(2,higherPerm) == Convert.ToInt32(permision) || Math.Pow(2, higherPerm) == Convert.ToInt32(PermisionLevel.Admins))
                {
                    return await Task.FromResult(true);
                }
                permLvl -= Convert.ToInt16(Math.Pow(2, higherPerm));
            }
            return await Task.FromResult(false);
        }
        public async Task<List<UInt64>> RoleHasPermision(UInt64 GuidId, PermisionLevel permision)
        {
            var PermisionNumber = _context.ModerationRoles
                .Where(x => x.id == GuidId)
                .Where(x => x.PermisionLevel >= Convert.ToInt32(permision))
                .Select(x => x.RoleId)
                .ToList();
            return await Task.FromResult(PermisionNumber);
        }
        public async Task AddUserPermision(UInt64 GuidId, UInt64 RoleId, PermisionLevel permision)
        {
            var PermisionNumber = Convert.ToInt32(permision);
            var Permisions = await _context.ModerationRoles
                .FindAsync(GuidId, RoleId);
            if (Permisions == null)
            {
                _context.Add(new ModerationRole { id = GuidId, RoleId = RoleId, PermisionLevel = PermisionNumber });
            }
            else
            if (Permisions.PermisionLevel < PermisionNumber)
            {
                Permisions.PermisionLevel = PermisionNumber;
            }
            _context.SaveChanges();
        }
    }
}

