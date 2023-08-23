using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialAPI.Models;

namespace SocialAPI.Authentication
{
  public sealed class JwtProvider : IJwtProvider
  {
    private readonly IConfiguration _configuration;
    private readonly JwtOptions _options;
    public JwtProvider(IConfiguration configuration, IOptions<JwtOptions> options)
    {
        _configuration = configuration;
        _options = options.Value;
    }
    public string Generate(User user)
    {
        string Role = user.Email.Contains("@admin") ? "Admin" : "User";

        var claims = new Claim[] 
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UUID.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.Username),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, Role)
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)
            ),
            SecurityAlgorithms.HmacSha256
        );
        
        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            DateTime.UtcNow.AddHours(1),
            signingCredentials
        );

        string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;

    }
  }
}