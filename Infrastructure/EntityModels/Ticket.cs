using DataAccess.Interfaces;

namespace DataAccess.EntityModels
{
	public class Ticket : IEntity
	{
		public int Id { get; set; }

		public string UserId { get; set; }
		public User User { get; set; }

		public int SeatId { get; set; }
		public Seat Seat { get; set; }

		public int SessionId { get; set; }
		public Session Session { get; set; }

		public DateTime PurchaseTime { get; set; } = DateTime.UtcNow;
	}
}
