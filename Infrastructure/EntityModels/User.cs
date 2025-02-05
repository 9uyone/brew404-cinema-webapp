using DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.EntityModels
{
	public class User : IdentityUser, IEntity {
		int IEntity.Id { get; set; }
		public string Role { get; set; }
	}
}
