using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;

namespace WebApp.Controllers.Admin
{
	[Authorize(Roles = "Admin")]
	[Area("admin")]
	public class PanelController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
