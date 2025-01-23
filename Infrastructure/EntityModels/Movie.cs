using DataAccess.Interfaces;

namespace DataAccess.Models
{
	public class Movie : IEntity
	{
		public Guid Id { get; set ; }
		public string? Title { get; set; }
		public string? Overview { get; set; }

	}
}
