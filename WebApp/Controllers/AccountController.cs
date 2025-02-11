using BusinessLogic.DTOs.Auth;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Toycloud.AspNetCore.Mvc.ModelBinding;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class AccountController : Controller
	{
		private readonly AccountService _accountService;

		public AccountController(AccountService accountService)
		{
			_accountService = accountService;
		}

		[HttpGet]
		public IActionResult Login(string? returnUrl)
		{
			TempData["ReturnUrl"] = returnUrl;
			//return View();
			return View("_LoginModal", new LoginDTO());
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromBodyOrDefault] LoginDTO model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _accountService.LoginUserAsync(model);

			if (!result.Succeeded)
			{
				return BadRequest("Неправильні дані для входу");
			}

			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> Register([FromBodyOrDefault] RegisterDTO model)
		{
			var result = await _accountService.RegisterUserAsync(model);
			
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!result.Succeeded)
			{
				return BadRequest(result.Errors.Select(result => result.Description));
			}

			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _accountService.LogoutAsync();
			return RedirectToAction("Index", "Home");
		}

		public IActionResult Profile()
		{
			ProfileViewModel model = new ProfileViewModel()
			{
				Name = User.Identity.Name
			};

			return View(model);
		}
	}
}
