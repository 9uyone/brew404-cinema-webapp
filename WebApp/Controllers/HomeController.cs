using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Diagnostics;
using BusinessLogic.TMDbServise;
using BusinessLogic.DTOs;
using BusinessLogic.TMDbService;
using System;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
		TMDbApiService service = new TMDbApiService();

		public HomeController()
        {
        }

        public async Task<IActionResult> Index()
        {
			return View(await service.GetMovies("venom"));
		}

        public async Task<IActionResult> Privacy()
        {
			List<CrewMemberDTO>? res = await service.GetCrew(335983);
			foreach(var el in res)
				Console.WriteLine(el.Name);
			return View();
        }

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			MovieDTO? movie = await service.GetMovieDetails(id);
			if(movie != null)
				movie.Crew = await service.GetCrew(movie.Id);
			return View(movie);
		}


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}