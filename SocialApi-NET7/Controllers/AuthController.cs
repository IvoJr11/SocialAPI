using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialAPI.Data;
using SocialAPI.Dtos;
using SocialAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SocialAPI.Authentication;
using SocialAPI.Services.AuthService;

namespace SocialAPI.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("register")]
		public async Task<ActionResult<ServiceResponse<GetUserDTO>>> Register(CreateUserDTO createUserDTO)
		{
			return Ok(await _authService.Register(createUserDTO));
		}

		[HttpPost("login")]
		public async Task<ActionResult<ServiceResponse<string>>> Login(LoginUserDTO loginUserDTO)
		{
			return Ok(await _authService.Login(loginUserDTO));
		}
	}
}
