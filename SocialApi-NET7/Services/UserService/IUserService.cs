using SocialAPI.Dtos;
using SocialAPI.Models;

namespace SocialAPI.Services.UserService
{
	public interface IUserService
	{
		Task<ServiceResponse<List<User>>> GetUsers();
		Task<ServiceResponse<GetUserDTO>> GetCurrentUser();
		Task<ServiceResponse<GetUserDTO>> GetUserById(Guid id);
		Task<ServiceResponse<GetUserDTO>> GetUserByUsername(string username);
		Task<ServiceResponse<GetUserDTO>> DeleteUser(Guid id);
		Task<ServiceResponse<GetUserDTO>> UpdateUser(UpdateUserDTO user);
		Task<ServiceResponse<string>> FollowUser(Guid id);
	}
}
