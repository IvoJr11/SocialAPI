using SocialAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SocialApi_NET7.Models
{
    public class Like
    {        
        public int UserID { get; set; }
        
        public int PostID { get; set;}

        [JsonIgnore]
        [Required]
        public User User { get; set; }

        [JsonIgnore]
        [Required]
        public Post Post { get; set; }
    }
}
