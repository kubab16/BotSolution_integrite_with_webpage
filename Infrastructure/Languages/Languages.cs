using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Languages
{
    public class Languages
    {
        private readonly Context _context;
        public Languages (Context context)
        {
            _context = context;
        }
        public async Task<bool> SetLanguage(ulong id, string langue)
        {
            var Lang = await _context.Language
                .FindAsync(langue);
            if(Lang != null)
            {
                var server = await _context.Servers.FindAsync(id);
                if (server != null)
                {
                    server.Langue = Lang;
                }
                else
                {
                    _context.Servers.Add(new Server { GuildId = id, Langue = Lang });
                }
                await _context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
        public async Task<bool> SetLanguage(ulong id, string langue,string FileName)
        {
            var Lang = await _context.Language
                .FindAsync(langue);
            if (Lang != null)
            {
                var server = await _context.Servers.FindAsync(id);
                if (server != null)
                {
                    server.Langue = Lang;
                }
                else
                {
                    _context.Servers.Add(new Server { GuildId = id, Langue = Lang });
                }
                await _context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            else
            {
                var server = await _context.Servers.FindAsync(id);
                if (server != null)
                {
                    server.Langue = new Language {name = langue, File = FileName };
                }
                else
                {
                    _context.Servers.Add(new Server { GuildId = id, Langue = new Language { name = langue, File = FileName } });
                }
                await _context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
        }
        public async Task<IConfigurationRoot> GetLanguage(ulong id)
        {
            var defaults = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("Languages/" + "English.json", false, true)
                        .Build();
            Server lang = await _context.Servers.FindAsync(id);
            if(lang != null)
            if(lang.Langue != null)
            {
                var langs = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("Languages/" + lang.Langue.File, false, true)
                        .Build();
                var result = langs ?? defaults;
                return await Task.FromResult(result);
            }
            

            return await Task.FromResult(defaults);
        }
        public async Task<List<Language>> GetAllLanges()
        {
            return await _context.Language.ToListAsync();
        }

    }
}
