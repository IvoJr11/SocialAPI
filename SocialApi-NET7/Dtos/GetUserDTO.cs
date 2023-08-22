using System.Text.Json.Serialization;
using SocialAPI.Dtos.PostDTO;
using SocialAPI.Models;

namespace SocialAPI.Dtos
{
	public class GetUserDTO
	{
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public DateTimeOffset CreationDate { get; set; }
		public ICollection<GetPostDTO> Posts { get; set; }
		public List<Followers> FollowerList { get; set; }
		public List<Followers> FollowingList { get;set; }
	}
}
