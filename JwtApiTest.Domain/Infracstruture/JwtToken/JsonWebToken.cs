using JwtApiTest.Domain.Infracstruture.JwtToken;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtApiTest.Domain.JwtToken
{
    public class JsonWebToken
    {
        public string AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public string TokenType { get; set; } = "bearer";
        public long ExpiresIn { get; set; }
    }
}
