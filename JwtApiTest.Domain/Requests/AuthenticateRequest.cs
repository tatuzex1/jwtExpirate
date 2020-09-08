using System;
using System.Collections.Generic;
using System.Text;

namespace JwtApiTest.Domain.Requests
{
    public class AuthenticateRequest
    {
        public string Email { get; }
        public string Password { get; }
        public string GrantType { get; }
        public string RefreshToken { get; }

        public AuthenticateRequest(string email, string password, string grantType, string refreshToken)
        {
            Email = email;
            Password = password;
            GrantType = grantType;
            RefreshToken = refreshToken;
        }


    }


}
