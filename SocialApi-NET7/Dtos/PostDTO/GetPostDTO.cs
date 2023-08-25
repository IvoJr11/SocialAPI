using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SocialAPI.Models;

namespace SocialAPI.Dtos.PostDTO
{
    public class GetPostDTO
    {
        public int Id { get; set; }
        [Required]
        public string Body { get; set; }

        public List<string> ImageUrls { get; set; }

        public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.UtcNow;

        [Required]
        public int Likes { get; set; } = 0;

        [Required]
		[JsonIgnore]
        public GetUserDTO Author { get; set; }
        public int AuthorId { get; set; }
    }
}