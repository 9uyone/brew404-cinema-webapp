namespace BusinessLogic.DTOs
{
	public class TicketDTO
	{
		public int? Id { get; set; }
		public string? UserId { get; set; }

		public int? SeatId { get; set; }
		public SeatDTO? Seat { get; set; }
		public int? SessionId { get; set; }
		public SessionDTO? Session { get; set; }
		public DateTime PurchaseTime { get; set; } = DateTime.Now;
	}
}