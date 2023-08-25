using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialAPI.Data;
using SocialAPI.Dtos;
using SocialAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SocialAPI.Utils;

namespace SocialAPI.Services.UserService
{
	public class UserService : IUserService
	{
		private readonly IMapper _mapper;
		private readonly DataContext _context;
		private readonly IHttpContextAccessor _httpContext;
		public UserService(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
			_context = context;
			_mapper = mapper;
			_httpContext = httpContextAccessor;
		}

		public async Task<ServiceResponse<List<GetUserDTO>>> GetUsers()
		{
			var serviceResponse = new ServiceResponse<List<GetUserDTO>>();
			var dbUsers = await _context.Users
				.Include(u=> u.FollowersList)
				.Include(u => u.Posts)
				.ToListAsync();
			serviceResponse.Data = _mapper.Map<List<GetUserDTO>>(dbUsers);
			serviceResponse.StatusCode = HttpStatusCode.OK;
			return serviceResponse;
		}

		async public Task<ServiceResponse<GetUserDTO>> GetCurrentUser()
		{
			var serviceResponse = new ServiceResponse<GetUserDTO>();

			var currentUser = await Utils.Utils.GetCurrentUser(_httpContext, _context, true);
			serviceResponse.Data = _mapper.Map<GetUserDTO>(currentUser);
			serviceResponse.Message = "Data found";
			serviceResponse.StatusCode = HttpStatusCode.OK;

			return serviceResponse;
		}

		public async Task<ServiceResponse<GetUserDTO>> GetUserById(int id)
		{
			var serviceResponse = new ServiceResponse<GetUserDTO>();
			var dbUser = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
			
			if (dbUser == null)
			{
				serviceResponse.StatusCode = HttpStatusCode.NotFound;
				serviceResponse.Message = "User not found";
				return serviceResponse;
			}

			serviceResponse.Data = _mapper.Map<GetUserDTO>(dbUser);
			serviceResponse.StatusCode = HttpStatusCode.OK;
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetUserDTO>> GetUserByUsername(string username)
		{
			var serviceResponse = new ServiceResponse<GetUserDTO>();
			var dbUser = await _context.Users.FirstOrDefaultAsync(user => user.Username == username);

			if (dbUser == null)
			{
				serviceResponse.StatusCode = HttpStatusCode.NotFound;
				serviceResponse.Message = "User not found";
				return serviceResponse;
			}

			serviceResponse.Data = _mapper.Map<GetUserDTO>(dbUser);
			serviceResponse.StatusCode = HttpStatusCode.OK;
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetUserDTO>> DeleteUser(int id)
		{
			var serviceResponse = new ServiceResponse<GetUserDTO>();
			var dbUser = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);

			if (dbUser == null)
			{
				serviceResponse.StatusCode = HttpStatusCode.NotFound;
				serviceResponse.Message = "User not found";
				return serviceResponse;
			}

			serviceResponse.Message = "User deleted";
			serviceResponse.StatusCode = HttpStatusCode.NoContent;
			_context.Users.Remove(dbUser);
			_context.SaveChanges();
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetUserDTO>> UpdateUser(UpdateUserDTO userToUpdate)
		{
			var serviceResponse = new ServiceResponse<GetUserDTO>();
			var dbUser = await _context.Users.FirstOrDefaultAsync(user => user.Email == userToUpdate.CurrentEmail);
			if(dbUser == null)
			{
				serviceResponse.StatusCode = HttpStatusCode.NotFound;
				serviceResponse.Message = "User not found";
				return serviceResponse;
			}
			if(!string.IsNullOrEmpty(userToUpdate.NewUsername))
			{
				dbUser.Username = userToUpdate.NewUsername;
			}
			if(!string.IsNullOrEmpty(userToUpdate.NewPassword))
			{
				string passwordBcrypt = BCrypt.Net.BCrypt.HashPassword(userToUpdate.NewPassword);
				dbUser.Password = passwordBcrypt;
			}
			serviceResponse.Message = "User updated successfully";
			serviceResponse.Data = _mapper.Map<GetUserDTO>(dbUser);
			serviceResponse.StatusCode = HttpStatusCode.OK;
			_context.Users.Update(dbUser);
			_context.SaveChanges();
			return serviceResponse;
		}

        public async Task<ServiceResponse<string>> FollowUser(int id)
        {
			var serviceResponse = new ServiceResponse<string>();
			var currentUser = await Utils.Utils.GetCurrentUser(_httpContext, _context, false);
			if(currentUser == null)
			{
				serviceResponse.Message = "User not found";
				serviceResponse.StatusCode = HttpStatusCode.NotFound;
				return serviceResponse;
			}
            var following = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
			if (following is null)
			{
				serviceResponse.Message = "User you trying to follow has not found";
				serviceResponse.StatusCode = HttpStatusCode.NotFound;
				return serviceResponse;
			}
			var newFollow = new Followers
			{
				Follower = currentUser,
				Following = following
			};
			_context.Add(newFollow);
			await _context.SaveChangesAsync();
			serviceResponse.Message = "Follow has been succesfully";
			serviceResponse.StatusCode = HttpStatusCode.OK;
			return serviceResponse;
        }
    }
}
