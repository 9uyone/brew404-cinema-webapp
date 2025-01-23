//using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Diagnostics;
using BusinessLogic.TMDbServise;
using BusinessLogic.TMDbService;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
			TMDbApiService service = new TMDbApiService();
			var json = await service.GetAsync(TmdbEndpoints.MovieDetails(27205));
			Console.WriteLine(json);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}