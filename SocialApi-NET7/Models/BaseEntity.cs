using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SocialApi_NET7.Models
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Body { get; set; } = string.Empty;

        public List<string> ImageUrls { get; set; } = new List<string>();

        public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.UtcNow;

        public int LikesCount { get; set; } = 0;

        public int CommentsCount { get; set; } = 0;
    }
}
