using DataAccess.Interfaces;

namespace DataAccess.EntityModels
{
	public class Hall : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		
		public int SeatsPerRow { get; set; }
		public int NumbOfRows { get; set; }

		public ICollection<Seat> Seats { get; set; } = new List<Seat>();
		public ICollection<Session> Sessions { get; set; } = new List<Session>();
	}
}
