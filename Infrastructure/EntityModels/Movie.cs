using DataAccess.Interfaces;

namespace DataAccess.Models
{
	public class Movie : IEntity
	{
		public Guid Id { get; set ; }
	}
}
