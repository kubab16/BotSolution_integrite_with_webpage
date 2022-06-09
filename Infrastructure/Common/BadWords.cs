using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common
{
    public class BadWords
    {
        private readonly Context _context;
        
        public BadWords(Context context)
        {
            _context = context;
        }

        public async Task<List<string>> GetBadWordListAsync()
        {
            return await _context.BadWords.Select(x => x.BadIssue).ToListAsync();
        }
        
        public async Task<bool> NewBadWordAsync(string badWord)
        {
            if ((await _context.BadWords.Where(x => x.BadIssue == badWord).FirstOrDefaultAsync())!= null)
            {
                _context.Add(new BadWord {BadIssue = badWord});
                await _context.SaveChangesAsync();
                return (await _context.BadWords.Where(x => x.BadIssue == badWord).FirstOrDefaultAsync())!= null;
            }

            return false;
        }

        public async Task<bool> RemoweBadWordAsync(string badWord)
        {
            BadWord BadWord = await _context.BadWords.Where(x => x.BadIssue == badWord).FirstOrDefaultAsync();
            if (BadWord != null)
            {
                _context.Remove(badWord);
                await _context.SaveChangesAsync();
                return (await _context.BadWords.Where(x => x.BadIssue == badWord).FirstOrDefaultAsync())== null;
            }

            return false;
        }
    }
}