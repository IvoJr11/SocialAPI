using SocialAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SocialApi_NET7.Utils.Validations;

namespace SocialApi_NET7.Models
{
    [LikeValidation]
    public class Like
    {        
        public int UserID { get; set; }
        
        public int? PostID { get; set;}
        
        public int? CommentID { get; set; }

        [JsonIgnore]
        [Required]
        public User User { get; set; }

        [JsonIgnore]
        [Required]
        public Post? Post { get; set; }

        [JsonIgnore]
        [Required]
        public Comment? Comment { get; set; }
    
        public bool IsValid()
        {
            return PostID.HasValue || CommentID.HasValue;
        }
    }
}
