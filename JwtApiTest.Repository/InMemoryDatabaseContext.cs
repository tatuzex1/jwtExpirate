using JwtApiTest.Domain.Infracstruture.JwtToken;
using JwtApiTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtApiTest.Repository
{
    public class InMemoryDatabaseContext
    {
        public ISet<User> Users { get; } = new HashSet<User>();
        public ISet<RefreshToken> RefreshTokens { get; } = new HashSet<RefreshToken>();
    }
}
