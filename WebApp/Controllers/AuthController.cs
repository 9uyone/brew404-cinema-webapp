using BusinessLogic.DTOs.Auth;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Toycloud.AspNetCore.Mvc.ModelBinding;

namespace WebApp.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class AuthController : Controller
	{
		private readonly AuthService _authService;

		public AuthController(AuthService authService)
		{
			_authService = authService;
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromBodyOrDefault] LoginDTO model)
		{
			var result = await _authService.LoginUser(model);

			if (!result)
				return Unauthorized();
			return Ok("Login successful");
		}

		[HttpPost]
		public IActionResult Logout()
		{
			_authService.Logout();
			return Ok("Logout successful");
		}

		[HttpPost]
		public async Task<IActionResult> Register([FromBodyOrDefault] RegisterDTO model)
		{
			var result = await _authService.RegisterUser(model);

			if (!result.Succeeded)
				return BadRequest(result.Errors);
			return Ok("Registration successful");
		}
	}
}
