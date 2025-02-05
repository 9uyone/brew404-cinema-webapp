using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;

namespace WebApp.Controllers.Admin
{
	//[Area("admin")]
	[Authorize(Roles = "Admin")]
	[Route("admin/[Controller]")]
	public class PanelController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
