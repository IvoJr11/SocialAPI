using System.ComponentModel.DataAnnotations;

namespace SocialAPI.Dtos
{
	public class UpdateUserDTO
	{
		[Required]
		public string CurrentEmail { get; set; }
		public string? NewUsername { get; set; }
		public string? NewPassword { get; set; }
	}
}
