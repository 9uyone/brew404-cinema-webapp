
namespace BusinessLogic.DTOs.User
{
	public class UpdateUserDTO
	{
		public string Email { get; set; } = null!;
		public DateOnly BirthDate { get; set; }
		public string PhoneNumber { get; set; } = null!;
	}
}
