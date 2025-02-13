using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs.Auth
{
	public class LoginDTO
	{
		public string Email { get; set; }

		public string Password { get; set; }
	}
}
