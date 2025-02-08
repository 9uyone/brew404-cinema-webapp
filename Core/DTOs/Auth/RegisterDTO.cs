using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs.Auth
{
	//[BindProperties]
	public class RegisterDTO
	{
		//[Required]
		public string Email { get; set; }

		//[Required]
		public string Password { get; set; }

		//[Required]
		public string ConfirmPassword { get; set; }

		//[Required]
		public string UserName { get; set; }
	}
}
