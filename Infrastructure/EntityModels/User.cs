using DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.EntityModels
{
	public class User : IdentityUser, IEntity
	{
		int IEntity.Id { get; set; }
		//string Id { get; set; }

		public string Role { get; set; }

		public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
	}
}
