using System;
using System.Collections.Generic;
using System.Text;

namespace JwtApiTest.Domain.Infracstruture.JwtToken
{
    public class RefreshToken
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
