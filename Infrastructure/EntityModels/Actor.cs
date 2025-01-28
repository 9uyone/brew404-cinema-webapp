using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.EntityModels
{
	public class Actor : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string? Character { get; set; }
		public string? AvatarUrl { get; set; }

		public ICollection<Movie> Movies { get; set; } = new List<Movie>(); 
	}
}
