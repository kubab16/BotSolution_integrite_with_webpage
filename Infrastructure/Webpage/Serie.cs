using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Webpage
{
    public class Serie
    {
        private readonly Context _context;
        public Serie(Context context)
        {
            _context = context;
        }
        /// <summary>
        /// Get all series
        /// </summary>
        /// <returns>List of Infrastructure.Series </returns>
        public async Task<List<Infrastructure.Series>> GetAllSeries()
        {
            var series = await _context.series.ToListAsync();
            return await Task.FromResult(series);
        }
        private bool HasTag(string series, string tags)
        {
            var tag = tags.Split(",");
            foreach (var t in tag)
            {
                if (!series.Contains(t))
                    return false;
            }
            return true;
        }
        private bool HasTag(string series, string[] tags)
        {
            foreach (var t in tags)
            {
                if (!series.Contains(t))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Get all series fith have tags
        /// </summary>
        /// <param name="tags">Tags seperate ','</param>
        /// <returns></returns>
        public async Task<List<Infrastructure.Series>> GetAllSeries(string tags)
        {
            var series = await _context.series.Where(x => HasTag(x.tags, tags)).ToListAsync();
            return await Task.FromResult(series);
        }
        /// <summary>
        /// Get all series fith have tags
        /// </summary>
        /// <param name="tags">Tags</param>
        /// <returns></returns>
        public async Task<List<Infrastructure.Series>> GetAllSeries(string[] tags)
        {
            var series = await _context.series.Where(x => HasTag(x.tags, tags)).ToListAsync();
            return await Task.FromResult(series);
        }
        public async Task AddSeries(string name, string tags, byte[] image = null, string description = null)
        {
            _context.series.Add(new Series { name = name, tags = tags, image = image, description = description });
            await _context.SaveChangesAsync();
        }
        public async Task ModyfyTags(int id, string tags)
        {
            var find = await _context.series
                .FindAsync(id);
            find.tags = tags;
            await _context.SaveChangesAsync();
        }
        public async Task ModyfyImage(int id, byte[] image)
        {
            var find = await _context.series
                .FindAsync(id);
            find.image = image;
            await _context.SaveChangesAsync();
        }
        public async Task ModyfyDescriptions(int id, string descriptions)
        {
            var find = await _context.series
                .FindAsync(id);
            find.description = descriptions;
            await _context.SaveChangesAsync();
        }
        public async Task RemoveSeries(int id)
        {
            _context.series
                .Remove(entity: await _context.series.FindAsync(id));
            await _context.SaveChangesAsync();
        }
        public async Task<double> GetRating(ulong SerieId)
        {
            var Rating = await _context.Rating
               .Where(x => x.seriesId == SerieId)
               .Select(x => x.raiting)
               .ToListAsync();
            var Avg = Rating.Average();
            return await Task.FromResult(Avg);
        }
        public async Task AddRating(ulong SeriesId, ulong userId, double raiting)
        {
            var raitings = await _context.Rating
                .Where(x => x.userId == userId)
                .Where(x => x.seriesId == SeriesId)
                .FirstOrDefaultAsync();
            if (raitings == null)
            {
                _context.Rating
                    .Add(
                    new SeriesRating { seriesId = SeriesId, userId = userId, raiting = raiting }
                    );
            }
            else
            {
                (await _context.Rating
                    .FindAsync(raitings))
                    .raiting = raiting;
            }
            await _context.SaveChangesAsync();
        }
        public async Task AddEpisode(ulong SeriesId, string[] name)
        {
            var context = _context.episodes;
            var count = _context.episodes
                .Where(x => x.SeriesId == SeriesId)
                .Count() + 1 ;
            foreach(var episode in name)
            {
                context.Add(new Episode { name = episode, Number = count, SeriesId = SeriesId });
                count++;
            }
            await _context.SaveChangesAsync();
        }
        public async Task ChangeNameEpisode (ulong EpisodeId, string newName)
        {
            var episode = await _context.episodes
                .FindAsync(EpisodeId);
            if(episode != null)
            {
                episode.name = newName;
                await _context.SaveChangesAsync();
            }
        }
       
        public async Task AddPlayer(ulong EpisodeId, ulong UserId, Players[] player)
        {
            var context = _context.players;

            foreach (var play in player)
            {
                context.Add(new player { EpisodeID = EpisodeId,name = play.Name,link = play.Link, UserId = UserId});
            }
            await _context.SaveChangesAsync();
        }
        public async Task RemovePlayer(ulong PlayerId)
        {
            var player = await _context.players
                .FindAsync(PlayerId);
            if(player != null)
            {
                _context.players
                    .Remove(player);
                await _context.SaveChangesAsync();
            }
        }
    }
    public class Players
    {
        public string Name { get; set; }
        public string Link { get; set; }
    }
}