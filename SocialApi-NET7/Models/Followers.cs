using System.Text.Json.Serialization;

namespace SocialAPI.Models
{
    public class Followers
    {
        [JsonIgnore]
        public User Follower { get; set; }
        public int FollowerID { get; set; }
        [JsonIgnore]
        public User Following { get; set; }
        public int FollowingID { get; set; }
    }
}
