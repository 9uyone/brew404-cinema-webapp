using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataAccess.EntityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BusinessLogic.DTOs.Auth;

namespace BusinessLogic.Services
{
	public class AccountService
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<bool> LoginUserAsync(LoginDTO model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
				return false;
			
			var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
			return result.Succeeded;
		}

		public async Task<IdentityResult> RegisterUserAsync(RegisterDTO model)
		{
			if (await _userManager.FindByEmailAsync(model.Email) != null)
				return IdentityResult.Failed(new IdentityError { Description = "Email already exists" });

			if (model.Password != model.ConfirmPassword)
				return IdentityResult.Failed(new IdentityError { Description = "Passwords do not match" });

			if (model.Password.Length < 6)
				return IdentityResult.Failed(new IdentityError { Description = "Password must be at least 6 characters long" });

			var result = await _userManager.CreateAsync( new User { UserName = model.UserName, Email = model.Email, Role="User" }, model.Password);
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