using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialAPI.Data;
using SocialAPI.Dtos.PostDTO;
using SocialAPI.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using SocialAPI.Dtos;
using SocialApi_NET7.Models;

namespace SocialAPI.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        public PostService(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContextAccessor;
        }

        public async Task<ServiceResponse<List<GetPostDTO>>> GetPosts()
        {
            var serviceResponse = new ServiceResponse<List<GetPostDTO>>();
            var dbPosts = await _context.Posts.ToListAsync();
            if(dbPosts.Count <= 0)
            {
                serviceResponse.StatusCode = HttpStatusCode.NotFound;
                serviceResponse.Message = "Has been an error with http request";
                return serviceResponse;
            }
            serviceResponse.Data = dbPosts.Select(post =>
                _mapper.Map<GetPostDTO>(post)
            ).ToList();
            serviceResponse.StatusCode = HttpStatusCode.OK;
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetPostDTO>> GetPostById(int id)
        {
            var serviceResponse = new ServiceResponse<GetPostDTO>();
            var dbPost = await _context.Posts.FirstOrDefaultAsync(user => user.Id == id);
            if(dbPost == null)
            {
                serviceResponse.StatusCode = HttpStatusCode.NotFound;
                serviceResponse.Message = "Post not found";
                return serviceResponse;
            }
            serviceResponse.Data = _mapper.Map<GetPostDTO>(dbPost); 
            serviceResponse.StatusCode = HttpStatusCode.OK;
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPostDTO>> CreatePost(CreatePostDTO createPostDTO)
        {
            var serviceResponse = new ServiceResponse<GetPostDTO>();

            var currentUser = await Utils.Utils.GetCurrentUser(_httpContext, _context, true);
            if(currentUser == null)
            {
                serviceResponse.Message = "User not found";
                serviceResponse.StatusCode = HttpStatusCode.NotFound;
                return serviceResponse;
            }

            var newPost = new Post
            {
                Body = createPostDTO.Body,
                AuthorId = currentUser.Id,
                Author = currentUser
            };

            currentUser.Posts.Add(newPost);
            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<GetPostDTO>(newPost);
            serviceResponse.Message = "created successfully";
            serviceResponse.StatusCode = HttpStatusCode.Created;

            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> LikePost(int post_id)
        {
            var serviceResponse = new ServiceResponse<bool>();
            var currentUser = await Utils.Utils.GetCurrentUser(_httpContext, _context, false);
            var currentPost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == post_id);
            
            if(currentUser is null)
            { 
                serviceResponse.Message = "User not found";
                serviceResponse.StatusCode= HttpStatusCode.NotFound;
                return serviceResponse;
            }

            if(currentPost is null)
            {
                serviceResponse.Message = "The post that you're trying to like was not found";
                serviceResponse.StatusCode= HttpStatusCode.NotFound;
                return serviceResponse;
            }
            
            var like = new Like
            {
                Post = currentPost,
                User = currentUser
            };

            currentPost.LikesCount += 1;
            _context.Add(like);
            await _context.SaveChangesAsync();
            
            serviceResponse.Data = true;
            serviceResponse.StatusCode = HttpStatusCode.OK;
            return serviceResponse;
        }
    }
}