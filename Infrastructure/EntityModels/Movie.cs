using DataAccess.EntityModels;
using DataAccess.Interfaces;

namespace DataAccess.Models
{
	public class Movie : IEntity
	{
		public int Id { get; set ; }
		public string Title { get; set; } = null!;
		public string? Overview { get; set; }
		public int RunTime { get; set; }

		public string? ImageUrl { get; set; }
		public string? BackgroundUrl { get; set; }
		public string? TrailerUrl { get; set; }

		public DateTime ReleaseDate { get; set; }

		public ICollection<Genre> Genres { get; set; } = new List<Genre>();
		public ICollection<CrewMember> Crew { get; set; } = new List<CrewMember>();
		public ICollection<Session> Sessions { get; set; } = new List<Session>();
	}
}