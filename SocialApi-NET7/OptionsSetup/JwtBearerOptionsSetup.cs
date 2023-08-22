using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialAPI.Authentication;

namespace SocialAPI.OptionsSetup
{
  public class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
  {
    private readonly JwtOptions _jwtOptions;
    private readonly IConfiguration _configuration;
    public JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions, IConfiguration configuration)
    {
      _jwtOptions = jwtOptions.Value;
      _configuration = configuration;
    }

    public void Configure(JwtBearerOptions options)
    {
      options.TokenValidationParameters = new()
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = _jwtOptions.Issuer,
        ValidAudience = _jwtOptions.Audience,
        IssuerSigningKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey))
      };
    }

    public void Configure(string? name, JwtBearerOptions options) => Configure(options);
  }
}