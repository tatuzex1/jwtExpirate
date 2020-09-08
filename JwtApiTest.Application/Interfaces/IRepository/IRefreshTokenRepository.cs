using JwtApiTest.Domain.Infracstruture.JwtToken;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JwtApiTest.Application.Interfaces.IRepository
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> Get(string username);
        Task Save(RefreshToken refreshToken);
    }
}
