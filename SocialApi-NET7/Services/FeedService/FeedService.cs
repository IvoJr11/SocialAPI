using SocialAPI.Models;

namespace SocialAPI.Services.FeedService
{
    public class FeedService : IFeedService
    {
        public Task<ServiceResponse<List<Post>>> getFeed()
        {
            throw new NotImplementedException();
        }
    }
}