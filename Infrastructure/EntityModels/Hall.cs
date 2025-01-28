using DataAccess.Interfaces;


namespace DataAccess.EntityModels
{
	public class Hall : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public int TotalSeats { get; set; }

		public ICollection<Session> Sessions { get; set; } = new List<Session>();
	}
}
