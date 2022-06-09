using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using Webpage.Models;
using Infrastructure;
using Infrastructure.Webpage;

namespace Webpage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context _context;
        private readonly Episodes _episode;

        public HomeController(ILogger<HomeController> logger, Context context)
        {
            _logger = logger;
            _context = context;
            _episode = new Episodes(context);    
        }

        public async Task<IActionResult> IndexAsync()
        {
            var episodes = await _episode.GetAllEpisodesAsync();

            return View(episodes);
        }

        public IActionResult Privacy()
        {
            return View();
        }       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
