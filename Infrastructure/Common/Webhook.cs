using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Bot
{
    public class Webhook
    {
        private readonly Context _context;

        public Webhook(Context context)
        {
            _context = context;
        }

        public async Task AddWebhookAsync(ulong GuidId, string WebhookToken, ulong WebhookId, string name)
        {
            var guild = await _context.Servers.FirstOrDefaultAsync(x => x.GuildId == GuidId);
            _context.Webhooks.Add(new Webhooks { Server = guild,IdServer = GuidId , Name = name, Token = WebhookToken, Id = WebhookId });
            await _context.SaveChangesAsync();
        }


        public async Task RemoveWebhook(ulong GuidId, string WebhookToken)
        {
            _context.Webhooks
                .Remove(entity: await _context.Webhooks.FindAsync(WebhookToken,GuidId));
            await _context.SaveChangesAsync();
        }

        public async Task<List<Webhooks>> GetWebhooks(ulong GuidId)
        {
            var result = await _context.Webhooks
                .Where(x => x.IdServer == GuidId)
                .ToListAsync();
            return await Task.FromResult(result);
        }

        public async Task<Webhooks> GetWebhook(ulong GuidId, string name)
        {
            var result = await _context.Webhooks
                .Where(x => x.IdServer == GuidId)
                .Where(x => x.Name == name).FirstOrDefaultAsync();
            return await Task.FromResult(result);
        }
    }
}
