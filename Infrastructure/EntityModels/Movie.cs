using DataAccess.EntityModels;
using DataAccess.Interfaces;

namespace DataAccess.Models
{
	public class Movie : IEntity
	{
		public int Id { get; set ; }
		public string Title { get; set; } = null!;
		public string? Overview { get; set; }

		public string? ImageUrl { get; set; }
		public string? TrailerUrl { get; set; }
		public string? BackgroundUrl { get; set; }

		public DateTime ReleaseDate { get; set; }

		public ICollection<Genre> Genres { get; set; } = new List<Genre>();
		public ICollection<Credit> Credits { get; set; } = new List<Credit>();
		public ICollection<Session> Sessions { get; set; } = new List<Session>();
	}
}