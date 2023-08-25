using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SocialAPI.Models
{
	public class User
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		public string Username { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }		

		[Required]
		public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.UtcNow;
		
		public ICollection<Post> Posts { get; } = new List<Post>();
		public List<Followers> FollowingList { get; set; } = new List<Followers>();
        public List<Followers> FollowersList { get; set; } = new List<Followers>();
    }
}
