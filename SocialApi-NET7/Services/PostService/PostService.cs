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
        public async Task<ServiceResponse<GetPostDTO>> GetPostById(Guid id)
        {
            var serviceResponse = new ServiceResponse<GetPostDTO>();
            var dbPost = await _context.Posts.FirstOrDefaultAsync(user => user.UUID == id);
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

            if(_httpContext?.HttpContext is null)
            {
                serviceResponse.StatusCode = HttpStatusCode.BadRequest;
                serviceResponse.Message = "Has been an error, try again";
                return serviceResponse;
            }

            var nameToToken = _httpContext.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Name);
            if(nameToToken == null)
            {
                serviceResponse.StatusCode = HttpStatusCode.NotFound;
                serviceResponse.Message = "The user is not authenticated";
                return serviceResponse;
            }
            
            var currentUser = await _context.Users.Include(u => u.Posts).FirstOrDefaultAsync(user => user.Username == nameToToken); 
            if(currentUser is null)
            {
                serviceResponse.StatusCode = HttpStatusCode.NotFound;
                serviceResponse.Message = "The user who is trying make a post was not found";
                return serviceResponse;
            }

            var newPost = new Post
            {
                Body = createPostDTO.Body,
                AuthorUUID = currentUser.UUID,
                Author = currentUser
            };

            if(_context.Posts is null)
            {
                serviceResponse.Message = "No hay posts xd";
                return serviceResponse;
            }

            currentUser.Posts.Add(newPost);
            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<GetPostDTO>(newPost);
            serviceResponse.Message = "created successfully";
            serviceResponse.StatusCode = HttpStatusCode.Created;

            return serviceResponse;
        }
    }
}