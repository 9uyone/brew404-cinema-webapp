using BusinessLogic.DTOs.Auth;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Toycloud.AspNetCore.Mvc.ModelBinding;

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
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromForm] LoginDTO model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _accountService.LoginUserAsync(model);
			if (!result)
			{
				ModelState.AddModelError("Login", "Invalid login attempt");
				return View(model);
			}

			return string.IsNullOrEmpty(TempData["ReturnUrl"]?.ToString()) 
				? RedirectToAction("Index", "Home") 
				: Redirect(TempData["ReturnUrl"].ToString());
		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await _accountService.LogoutAsync();
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> Register([FromBodyOrDefault] RegisterDTO model)
		{
			var result = await _accountService.RegisterUserAsync(model);

			if (!result.Succeeded)
				return BadRequest(result.Errors);
			return Ok("Registration successful");
		}
	}
}
