using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialAPI.Dtos.PostDTO;
using SocialAPI.Models;

namespace SocialAPI.Services.PostService
{
    public interface IPostService
    {
        Task<ServiceResponse<List<GetPostDTO>>> GetPosts();
        Task<ServiceResponse<GetPostDTO>> GetPostById(int id);
        Task<ServiceResponse<GetPostDTO>> CreatePost(CreatePostDTO createPostDTO);
    }
}