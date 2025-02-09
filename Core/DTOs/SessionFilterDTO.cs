
namespace BusinessLogic.DTOs
{
	public class SessionFilterDTO
	{
		public int? MovieId { get; set; }
		public DateTime? Date { get; set; }
		public string? SortBy { get; set; }
		public bool Descending = false;
	}
}
