using Microsoft.EntityFrameworkCore;
using SocialAPI.Data;
using SocialAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SocialAPI.Utils
{
    public class Utils
    {
        public async static Task<User?> GetCurrentUser(IHttpContextAccessor httpContext, DataContext context , bool withPosts)
        {
            if(httpContext.HttpContext is null)
                return null;

            string nameToToken = httpContext.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Name);
            
            var query = context.Users.Where(u => u.Username == nameToToken);
            
            if (withPosts)
                query = query.Include(u => u.Posts);
            
            
            User currentUser = await query.FirstOrDefaultAsync();
           
            return currentUser;
        }
    }
}
