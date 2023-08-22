using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialAPI.Models;

namespace SocialAPI.Authentication
{
    public interface IJwtProvider
    {
        string Generate(User user);
    }
}