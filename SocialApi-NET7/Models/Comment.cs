using SocialAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SocialApi_NET7.Models
{
    public class Comment : BaseEntity
    {
        public int AuthorId { get; set; }

        [Required]
        public User? Author { get; set; }

        public int PostId { get; set; }

        [Required]
        public Post? Post { get; set; }     
    }
}
