using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs.Auth
{
	public class RegisterDTO
	{
		public string Email { get; set; }

		public string Password { get; set; }

		public string ConfirmPassword { get; set; }

		public string UserName { get; set; }
	}
}
