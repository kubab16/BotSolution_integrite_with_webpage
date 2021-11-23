using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public enum TypeComand
    {
        moderation,
        miusic,
        Inoformation,
        Funny
    }

    public class Comand
    {
        private readonly Context _context;

        public Comand(Context context)
        {
            _context = context;
        }
        /// <summary>
        /// Enable comend section
        /// </summary>
        /// <param name="id">Guid's id</param>
        /// <param name="type">Section type</param>
        /// <returns></returns>
        public async Task EnableComand(ulong id, TypeComand type)
        {
            var Comand = await _context.Comands
                .FindAsync(id);
            if (Comand == null)
            {
                var guild = await _context.Servers.FirstOrDefaultAsync(x => x.GuildId == id);
                _context.Add(new Comands {Miusic = true, Moderation = true, Funny = true, Information = true });
                await _context.SaveChangesAsync();
                Comand = await _context.Comands
                    .FindAsync(id);
            }
            switch (type)
            {
                case TypeComand.miusic:
                    Comand.Miusic = true;
                    break;
                case TypeComand.moderation:
                    Comand.Moderation = true;
                    break;
                case TypeComand.Funny:
                    Comand.Funny = true;
                    break;
                case TypeComand.Inoformation:
                    Comand.Information = true;
                    break;
            }
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Disable comend section
        /// </summary>
        /// <param name="id">Guid's id</param>
        /// <param name="type">Section type</param>
        /// <returns></returns>
        public async Task DiscambleComand(ulong id, TypeComand type)
        {
            var Comand = await _context.Comands
                .FindAsync(id);
            if (Comand == null)
            {
                var guild = await _context.Servers.FirstOrDefaultAsync(x => x.GuildId == id);
                _context.Add(new Comands { Miusic = true, Moderation = true, Funny = true, Information = true });
                await _context.SaveChangesAsync();
                Comand = await _context.Comands
                    .FindAsync(id);
            }
            switch (type)
            {
                case TypeComand.miusic:
                    Comand.Miusic = false;
                    break;
                case TypeComand.moderation:
                    Comand.Moderation = false;
                    break;
                case TypeComand.Funny:
                    Comand.Funny = false;
                    break;
                case TypeComand.Inoformation:
                    Comand.Information = false;
                    break;
            }
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Get comend section config (enable/disable)
        /// </summary>
        /// <param name="id">Guid's id</param>
        /// <param name="type">Section type</param>
        /// <returns>Boolen value (True - enable; False - disable)</returns>
        public async Task<bool> GetComendConfig(ulong id, TypeComand type)
        {
            var Comands = await _context.Comands
                .FindAsync(id);
            if (Comands == null)
            {
                var guild = await _context.Servers.FirstOrDefaultAsync(x => x.GuildId == id);
                _context.Add(new Comands {Miusic = true, Moderation = true, Funny = true, Information = true });
                await _context.SaveChangesAsync();
                Comands = await _context.Comands
                    .FindAsync(id);
            }
            bool comand = false;
            switch (type)
            {
                case TypeComand.miusic:
                    comand = await _context.Comands
                        .Where(x => x.Id == id)
                        .Select(x => x.Miusic)
                        .FirstOrDefaultAsync();

                    break;
                case TypeComand.moderation:
                    comand = await _context.Comands
                        .Where(x => x.Id == id)
                        .Select(x => x.Moderation)
                        .FirstOrDefaultAsync();
                    break;
                case TypeComand.Funny:
                    comand = await _context.Comands
                        .Where(x => x.Id == id)
                        .Select(x => x.Funny)
                        .FirstOrDefaultAsync();
                    break;
                case TypeComand.Inoformation:
                    comand = await _context.Comands
                        .Where(x => x.Id == id)
                        .Select(x => x.Information)
                        .FirstOrDefaultAsync();
                    break;
            }          
            return comand;
        }

        public async Task<Comands> GetComendsConfig(ulong id)
        {
            var comand = await _context.Comands.FindAsync(id);
            return await Task.FromResult(comand);
        }
    }
}
