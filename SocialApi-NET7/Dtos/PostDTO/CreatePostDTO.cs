using System.ComponentModel.DataAnnotations;

namespace SocialAPI;

public class CreatePostDTO
{
  [Required]
  public string Body { get; set;}
  public List<string>? ImageUrls { get; set;}
}
