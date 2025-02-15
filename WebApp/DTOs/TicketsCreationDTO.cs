namespace WebApp.DTOs
{
	public class TicketsCreationDTO
	{
		public string UserId { get; set; }
		public int SessionId { get; set; }
		public IEnumerable<Tuple<int, int>> Seats { get; set; }
	}
}