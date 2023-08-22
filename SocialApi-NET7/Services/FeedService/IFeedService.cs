using SocialAPI.Models;

namespace SocialAPI.Services.FeedService
{
    public interface IFeedService
    {
        Task<ServiceResponse<List<Post>>> getFeed();
    }
}