using DataAccess.Interfaces;
using DataAccess.Models;


namespace DataAccess.EntityModels
{
	public class Genre : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;

		public ICollection<Movie> Movies { get; set; } = new List<Movie>();
	}
}
