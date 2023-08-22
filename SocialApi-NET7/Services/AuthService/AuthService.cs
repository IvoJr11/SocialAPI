using SocialAPI.Dtos;
using SocialAPI.Models;
using SocialAPI.Data;
using SocialAPI.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace SocialAPI.Services.AuthService
{
  public class AuthService : IAuthService
  {
    private readonly IConfiguration _configuration;
    private readonly DataContext _context; 
    private readonly IJwtProvider _jwtProvider;
    private readonly IMapper _mapper;
    public AuthService(IConfiguration configuration, DataContext context, IJwtProvider provider , IMapper mapper)
    {
        _configuration = configuration;
        _context = context;
        _jwtProvider = provider;
        _mapper = mapper;
    }
    
    public async Task<ServiceResponse<GetUserDTO>> Register(CreateUserDTO createUserDTO)
    {
        var serviceResponse = new ServiceResponse<GetUserDTO>();
        var isUsernameUsed = await _context.Users.FirstOrDefaultAsync(user => user.Username == createUserDTO.Username);
        var isEmailUsed = await _context.Users.FirstOrDefaultAsync(user => user.Email == createUserDTO.Email);
        if (createUserDTO.Username == null || createUserDTO.Username == "")
        {
            serviceResponse.StatusCode = HttpStatusCode.BadRequest;
            serviceResponse.Message = "Username cannot be empty";
            return serviceResponse;
        }
        if (isUsernameUsed != null)
        {
            serviceResponse.StatusCode = HttpStatusCode.BadRequest;
            serviceResponse.Message = "Username is already used";
            return serviceResponse;
        }
        if (createUserDTO.Email == null || createUserDTO.Email == "")
        {
            serviceResponse.StatusCode = HttpStatusCode.BadRequest;
            serviceResponse.Message = "Email cannot be empty";
            return serviceResponse;
        }
        if (isEmailUsed != null)
        {
            serviceResponse.StatusCode = HttpStatusCode.BadRequest;
            serviceResponse.Message = "Email is already used";
            return serviceResponse;
        }
        string passwordBcrypt = BCrypt.Net.BCrypt.HashPassword(createUserDTO.Password);
        var newUser = new User()
        {
            Username = createUserDTO.Username,
            Email = createUserDTO.Email,
            Password = passwordBcrypt
        };
        _context.Users.Add(newUser);
        _context.SaveChanges();
        serviceResponse.StatusCode = HttpStatusCode.Created;
        serviceResponse.Message = "User created succesfully";
        serviceResponse.Data = _mapper.Map<GetUserDTO>(newUser);

        return serviceResponse;
    }
    public async Task<ServiceResponse<string>> Login(LoginUserDTO loginUserDTO)
    {
        var serviceResponse = new ServiceResponse<string>();
        var dbUser = await _context.Users.FirstOrDefaultAsync(user => user.Username == loginUserDTO.Username);
    
        if(dbUser == null)
        {
            serviceResponse.Message = "User not found";
            serviceResponse.StatusCode = HttpStatusCode.BadRequest;
            return serviceResponse;
        }

        if(!BCrypt.Net.BCrypt.Verify(loginUserDTO.Password, dbUser.Password))
        {
            serviceResponse.Message = "Wrong password";
            serviceResponse.StatusCode = HttpStatusCode.BadRequest;
            return serviceResponse;
        }

        string token = _jwtProvider.Generate(dbUser);
        serviceResponse.Message = "Login successfully";
        serviceResponse.Data = token;
        serviceResponse.StatusCode = HttpStatusCode.OK;

        return serviceResponse;
    }
  }
}