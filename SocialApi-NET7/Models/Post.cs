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
        public string Body { get; set; } = string.Empty;

        public List<string> ImageUrls { get; set; } = new List<string>();

        public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.UtcNow;

        public int Likes { get; set; } = 0;

        public int AuthorId { get; set; }
        
        [Required]
        public User? Author { get; set; }

    }
}