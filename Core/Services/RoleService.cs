using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Services
{
	public class RoleService
	{
		private readonly RoleManager<IdentityRole> _roleManager;

		public RoleService(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}

		public List<IdentityRole> GetAllRoles()
		{
			return _roleManager.Roles.ToList();
		}

		public async Task<bool> CreateRoleAsync(string roleName)
		{
			if (await _roleManager.RoleExistsAsync(roleName))
				return false;

			var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
			return result.Succeeded;
		}
	}
}