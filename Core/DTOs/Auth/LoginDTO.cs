using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs.Auth
{
	public class LoginDTO
	{
		//[Required]
		//[EmailAddress]
		public string Email { get; set; }

		//[Required]
		//[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
