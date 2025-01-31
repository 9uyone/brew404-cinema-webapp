using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Admin
{
	[Route("admin/Panel")]
	public class PanelController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
