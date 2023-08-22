using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialAPI.Authentication
{
    public class JwtOptions
    {
        public string Issuer { get; init; } = "SocialAPI";
        public string Audience { get; init; } = "SocialAPI";
        public string SecretKey { get; init; } = "4xW6mMOgO+ViJeqYIen1TI/tgqXZhndWH0U3XT8IC10i7sxDGLS5thG6gFguf6ziCuYVo1TdvRu0+zX9X5lHxPM097tGILotwP6qoJa/od1WnJG3gO+i8VGMk3ykdjk3s7i9uT6dZ9FM7ya5XRuCaJkosDRhRcNyzVtubwdNH5BqT75iKSyfM4ClnH1noP8mDKdkMEE+NVvIX6l5eHK6nloHqtdog+T4uZB2fCcAjtyfxf/ThAyh/JShyePFvOx6k91ZPcmrqCIdIq7Qc4T4OZAlJAY0N5vf/B6C0qs279VUkILEbF4E7Q/RaFYKwu21ZR7Wum+Xt8933S85/OxBhNbvA2P/5THaMO2Y6f0/pLU=";
    }
}