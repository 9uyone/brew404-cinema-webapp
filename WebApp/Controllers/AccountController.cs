using BusinessLogic.DTOs.Auth;
using BusinessLogic.DTOs.User;
using BusinessLogic.Services;
using DataAccess.EntityModels;
using Microsoft.AspNetCore.Identity;
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
		private readonly TicketService _ticketService;
		private readonly UserManager<User> _userManager;

		public AccountController(AccountService accountService
			, TicketService ticketService
			, UserManager<User> userManager)
		{
			_accountService = accountService;
			_ticketService = ticketService;
			_userManager = userManager;
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
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _accountService.RegisterUserAsync(model);

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

		[HttpPost]
		public async Task<IActionResult> Update([FromForm, Bind(Prefix = "UpdateUser")] UpdateUserDTO updateDTO)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return Unauthorized();
			}

			await _accountService.UpdateUserAsync(user.Id, updateDTO);
			return RedirectToAction(nameof(Profile));
		}

		public async Task<IActionResult> Profile()
		{
			var user = await _userManager.GetUserAsync(User);

			var userTickets = await _ticketService.GetTicketByUserIdAsync(user?.Id);
			ProfileViewModel model = new ProfileViewModel()
			{
				User = await _userManager.GetUserAsync(User),
				PastTickets = userTickets.Item1.ToList(),
				CurrentTickets = userTickets.Item2.ToList(),
				UpdateUser = new UpdateUserDTO
				{
					UserName = user.UserName,
					Email = user.Email,
					PhoneNumber = user.PhoneNumber,
					BirthDate = user.BirthDate
				},
			};

			return View(model);
		}
	}
}
