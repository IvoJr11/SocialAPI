using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SocialAPI.Authentication;

namespace SocialAPI.OptionsSetup
{
  public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
  {
    private const string SectionName = "jwt"; 
    private readonly IConfiguration _configuration;

    public JwtOptionsSetup(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
      _configuration.GetSection(SectionName).Bind(options);
    }
  }
}