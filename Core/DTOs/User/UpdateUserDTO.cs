
namespace BusinessLogic.DTOs.User
{
	public class UpdateUserDTO
	{
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public DateOnly? BirthDate { get; set; }
		public string? PhoneNumber { get; set; }
	}
}
