using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialAPI.Dtos;
using SocialAPI.Models;
using SocialAPI.Services.UserService;

namespace SocialAPI.Controllers
{
	[Route("api/user")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[Authorize]
		[HttpGet("getAll")]
		public async Task<ActionResult<ServiceResponse<List<User>>>> GetAllUsers()
		{
			return Ok(await _userService.GetUsers());
		}

		[HttpGet("current")]
		public async Task<ActionResult<ServiceResponse<GetUserDTO>>> GetCurrentUser()
		{
			return Ok(await _userService.GetCurrentUser());
		}

		[HttpGet("id/{id}")]
		public async Task<ActionResult<ServiceResponse<GetUserDTO>>> GetUserById(int id)
		{
			return Ok(await _userService.GetUserById(id));
		}

		[HttpGet("username/{username}")]
		public async Task<ActionResult<ServiceResponse<GetUserDTO>>> GetUserByUsername(string username)
		{
			return Ok(await _userService.GetUserByUsername(username));
		}
		
		[HttpPatch("update")]
		public async Task<ActionResult<ServiceResponse<GetUserDTO>>> UpdateUser(UpdateUserDTO updateUserDTO)
		{
			return Ok(await _userService.UpdateUser(updateUserDTO));
		}

		[HttpDelete("delete")]
		public async Task<ActionResult<ServiceResponse<GetUserDTO>>> DeleteUser(int id)
		{
			return Ok(await _userService.DeleteUser(id));
		}

		[HttpPost("follow/{id}")]
		public async Task<ActionResult<ServiceResponse<string>>> FollowUser(int id)
		{
			return Ok(await _userService.FollowUser(id));
		}
	}
}
