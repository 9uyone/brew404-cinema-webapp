using AutoMapper;
using DataAccess.EntityModels;
using Microsoft.AspNetCore.Identity;
using BusinessLogic.DTOs.Auth;
using BusinessLogic.DTOs;

namespace BusinessLogic.Services
{
	public class AccountService
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly TicketService _ticketService;
		private readonly IMapper _mapper;

		public AccountService(UserManager<User> userManager
			, SignInManager<User> signInManager
			, TicketService ticketService
			, IMapper mapper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_ticketService = ticketService;
			_mapper = mapper;
		}

		public async Task<SignInResult> LoginUserAsync(LoginDTO model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
				return SignInResult.Failed;

			var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
			return result;
		}

		public async Task<List<GenreDTO>?> FavMovieGenres(string userId)
		{
			var userTickets = await _ticketService.GetTicketByUserIdAsync(userId);

			var genres = userTickets.Item1
				.SelectMany(t => t.Session.Movie.Genres)
				.Concat(userTickets.Item2.SelectMany(t => t.Session.Movie.Genres))
				.DistinctBy(g => g.Id)
				.ToList();

			return _mapper.Map<List<GenreDTO>>(genres);
		}

		public async Task<IdentityResult> RegisterUserAsync(RegisterDTO model)
		{
			if (await _userManager.FindByEmailAsync(model.Email) != null)
				return IdentityResult.Failed(new IdentityError { Description = "Користувач з такою поштою вже зареєстрований" });

			//var result = await _userManager.CreateAsync( new User { UserName = model.UserName, Email = model.Email, Role="User" }, model.Password);
			var result = await _userManager.CreateAsync(_mapper.Map<User>(model), model.Password);
			if (result.Succeeded)
				await _userManager.AddToRoleAsync(await _userManager.FindByEmailAsync(model.Email), "User");

			return result;
		}

		public async Task LogoutAsync()
		{
			await _signInManager.SignOutAsync();
		}
	}
}