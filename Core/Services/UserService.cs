using DataAccess.EntityModels;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Services
{
	public class UserService
	{
		private readonly UserManager<User> _userManager;

		public UserService(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public async Task<User?> GetUserByEmailAsync(string email)
		{
			return await _userManager.FindByEmailAsync(email);
		}

		public List<User> GetAllUsers()
		{
			return _userManager.Users.ToList();
		}

		public async Task<bool> AssignRoleAsync(string userId, string role)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null) return false;

			var result = await _userManager.AddToRoleAsync(user, role);
			return result.Succeeded;
		}
	}
}