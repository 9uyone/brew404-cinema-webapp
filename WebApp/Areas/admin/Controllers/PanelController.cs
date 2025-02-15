using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
