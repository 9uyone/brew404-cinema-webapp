using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.EntityModels
{
	public class CrewMember : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string? AvatarUrl { get; set; }

		public ICollection<Movie> Movies { get; set; } = new List<Movie>();
		public ICollection<Role> Roles { get; set; } = new List<Role>(); 
	}
}
