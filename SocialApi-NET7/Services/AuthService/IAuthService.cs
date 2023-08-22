using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialAPI.Dtos;
using SocialAPI.Models;

namespace SocialAPI.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<GetUserDTO>> Register(CreateUserDTO user);
        Task<ServiceResponse<string>> Login(LoginUserDTO user);
    }
}