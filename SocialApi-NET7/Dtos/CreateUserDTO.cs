﻿using System.ComponentModel.DataAnnotations;

namespace SocialAPI.Dtos
{
	public class CreateUserDTO
	{
		[Required]
		public string Username { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
