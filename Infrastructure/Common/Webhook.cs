using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Bot
{
    public class Webhook
    {
        private readonly MyBotContext _context;

        public Webhook(MyBotContext context)
        {
            _context = context;
        }

        public void AddWebhook(ulong GuidId, string WebhookToken, ulong WebhookId, string name)
        {
            _context.Webhooks.Add(new Webhooks { IdServer = GuidId, name = name, Token = WebhookToken, id = WebhookId });
            _context.SaveChangesAsync();
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
                .Where(x => x.name == name).FirstOrDefaultAsync();
            return await Task.FromResult(result);
        }
    }
}
