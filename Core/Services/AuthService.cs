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
	public class AuthService
	{
		public short TOKEN_EXPIRE_IN_HOURS { get; } = 3;

		private readonly UserManager<User> _userManager;
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public AuthService(UserManager<User> userManager, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
		{
			_userManager = userManager;
			_configuration = configuration;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<bool> LoginUser(LoginDTO model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
				return false;

			SetTokenInCookie(GenerateJwtToken(user));
			return true;
		}

		public async Task<IdentityResult> RegisterUser(RegisterDTO model)
		{
			if (await _userManager.FindByEmailAsync(model.Email) != null)
				return IdentityResult.Failed(new IdentityError { Description = "Email already exists" });

			if (model.Password != model.ConfirmPassword)
				return IdentityResult.Failed(new IdentityError { Description = "Passwords do not match" });

			if (model.Password.Length < 6)
				return IdentityResult.Failed(new IdentityError { Description = "Password must be at least 6 characters long" });

			var result = await _userManager.CreateAsync( new User { UserName = model.UserName, Email = model.Email, Role="Admin" }, model.Password);
			//if (result.Succeeded)
			//	await _userManager.AddToRoleAsync(await _userManager.FindByEmailAsync(model.Email), "User");
			return result;
		}

		private string GenerateJwtToken(User user)
		{
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim(ClaimTypes.Role, "User")
			};

			var token = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"],
				claims: claims,
				expires: DateTime.UtcNow.AddHours(TOKEN_EXPIRE_IN_HOURS),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		private void SetTokenInCookie(string token)
		{
			var options = new CookieOptions
			{
				HttpOnly = true,
				Secure = true,
				SameSite = SameSiteMode.Strict,
				Expires = DateTime.UtcNow.AddHours(TOKEN_EXPIRE_IN_HOURS)
			};

			_httpContextAccessor.HttpContext.Response.Cookies.Append("jwt", token, options);
		}

		public void Logout()
		{
			_httpContextAccessor.HttpContext.Response.Cookies.Delete("jwt");
		}
	}
}