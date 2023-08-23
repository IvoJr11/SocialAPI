using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SocialAPI.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Body { get; set; }

        public List<string> ImageUrls { get; set; } = new List<string>();

        public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.UtcNow;

        public int Likes { get; set; } = 0;

        public int AuthorId { get; set; }
        
        [Required]
		[JsonIgnore]
        public User? Author { get; set; }
        public string AuthorName => Author?.Username ?? "unknown";
        // public string AuthorName {
        //     get {
        //         return Author?.Username ?? "unknown";
        //     }
        //     set {}
        // } 
    }
}