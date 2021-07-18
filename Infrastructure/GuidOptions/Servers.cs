using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Servers
    {
        private readonly Context _context;

        public Servers(Context context)
        {
            _context = context;
        }
        /// <summary>
        /// Modify guild's comand prefix
        /// </summary>
        /// <param name="id">Guild ID</param>
        /// <param name="prefix">New prefix</param>
        /// <returns></returns>
        public async Task ModifyGuildPrefix(ulong id, string prefix)
        {
            var server = await _context.Servers
                .FindAsync(id);

            if (server == null)
                _context.Add(new Server { GuildId = id, Prefix = prefix });
            else
                server.Prefix = prefix;

            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Get guilds prefix
        /// </summary>
        /// <param name="id">Guild ID</param>
        /// <returns></returns>
        public async Task<string> GetGuildPrefix(ulong id)
        {
            var prefix = await _context.Servers
                .Where(x => x.GuildId == id)
                .Select(x => x.Prefix)
                .FirstOrDefaultAsync();
            return await Task.FromResult(prefix);
        }       

        /// <summary>
        /// Set channel logs
        /// </summary>
        /// <param name="id">Guild ID</param>
        /// <param name="channelId">Channel ID</param>
        /// <returns></returns>
        public async Task ModifyLogsAsync(ulong id, ulong channelId)
        {
            var server = await _context.Servers
                .FindAsync(id);

            if (server == null)
                _context.Add(new Server { GuildId = id, Logs = channelId });
            else
                server.Logs = channelId;

            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Clear logs channel
        /// </summary>
        /// <param name="id">Channel ID</param>
        /// <returns></returns>
        public async Task ClearLogsAsync(ulong id)
        {
            var server = await _context.Servers
                .FindAsync(id);

            server.Logs = 0;
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Get log channel ID
        /// </summary>
        /// <param name="id">Guid's ID</param>
        /// <returns></returns>
        public async Task<ulong> GetLogsAsync(ulong id)
        {
            var server = await _context.Servers
                .FindAsync(id);

            return await Task.FromResult(server.Logs);
        }
        /// <summary>
        /// Set welcome channel 
        /// </summary>
        /// <param name="id">Guid's ID</param>
        /// <param name="channelId">Channel ID</param>
        /// <returns></returns>
        public async Task ModifyWelcomeAsync(ulong id, ulong channelId)
        {
            var server = await _context.Servers
                .FindAsync(id);

            if (server == null)
                _context.Add(new Server { GuildId = id, ChannelId = channelId });
            else
                server.ChannelId = channelId;

            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Remove welcome channel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task ClearWelcomeAsync(ulong id)
        {
            var server = await _context.Servers
                .FindAsync(id);

            server.ChannelId = 0;
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Get welcome channel ID
        /// </summary>
        /// <param name="id">Guild ID</param>
        /// <returns></returns>
        public async Task<ulong> GetWelcomeAsync(ulong id)
        {
            var server = await _context.Servers
                .FindAsync(id);

            return await Task.FromResult(server.ChannelId);
        }
        /// <summary>
        /// Set background image url
        /// </summary>
        /// <param name="id">Guild's ID</param>
        /// <param name="url">Url image</param>
        /// <returns></returns>
        public async Task ModifyBackgroundAsync(ulong id, string url)
        {
            var webClient = new WebClient();
            var server = await _context.Servers
                .FindAsync(id);
            byte[] imageBytes = webClient.DownloadData(url);
            if (server == null)
            {

                _context.Add(new Server { GuildId = id, Background = imageBytes });
            }               
            else
                server.Background = imageBytes;

            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Set background image 
        /// </summary>
        /// <param name="id">Guild's ID</param>
        /// <param name="imageBytes">Image</param>
        /// <returns></returns>
        public async Task ModifyBackgroundAsync(ulong id, byte[] imageBytes)
        {
            var server = await _context.Servers
                .FindAsync(id);
            if (server == null)
            {

                _context.Add(new Server { GuildId = id, Background = imageBytes });
            }
            else
                server.Background = imageBytes;

            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Remove background image
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task ClearBackgroundAsync(ulong id)
        {
            var server = await _context.Servers
                .FindAsync(id);

            server.Background = null;
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Get background image
        /// </summary>
        /// <param name="id">Guild's ID</param>
        /// <returns></returns>
        public async Task<byte[]> GetBackgroundAsync(ulong id)
        {
            var server = await _context.Servers
                .FindAsync(id);
            return await Task.FromResult(server.Background);
        }
    }
}
