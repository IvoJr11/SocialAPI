using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialAPI.Dtos.PostDTO;
using SocialAPI.Models;
using SocialAPI.Services.PostService;

namespace SocialAPI.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [Authorize]
        [HttpGet("getAll")]
        public async Task<ActionResult<ServiceResponse<List<GetPostDTO>>>> GetPosts()
        {
            return Ok(await _postService.GetPosts());
        }

        [Authorize]
        [HttpGet("id/{id}")]
        public async Task<ActionResult<ServiceResponse<GetPostDTO>>> GetPostById(int id)
        {
            return Ok(await _postService.GetPostById(id));
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult<ServiceResponse<GetPostDTO>>> CreatePost(CreatePostDTO newPost)
        {
            return Ok(await _postService.CreatePost(newPost));
        }

        [Authorize]
        [HttpPost("like/{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> LikePost(int id)
        {
            return Ok(await _postService.LikePost(id));
        }
    }
}