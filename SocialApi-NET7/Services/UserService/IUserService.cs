using SocialAPI.Dtos;
using SocialAPI.Models;

namespace SocialAPI.Services.UserService
{
	public interface IUserService
	{
		Task<ServiceResponse<List<GetUserDTO>>> GetUsers();
		Task<ServiceResponse<GetUserDTO>> GetCurrentUser();
		Task<ServiceResponse<GetUserDTO>> GetUserById(int id);
		Task<ServiceResponse<GetUserDTO>> GetUserByUsername(string username);
		Task<ServiceResponse<GetUserDTO>> DeleteUser(int id);
		Task<ServiceResponse<GetUserDTO>> UpdateUser(UpdateUserDTO user);
		Task<ServiceResponse<string>> FollowUser(int id);
	}
}
