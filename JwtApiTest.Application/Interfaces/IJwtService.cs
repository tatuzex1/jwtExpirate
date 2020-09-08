using JwtApiTest.Domain.JwtToken;
using JwtApiTest.Domain.Models;

namespace JwtApiTest.Application.Interfaces
{
    public interface IJwtService
    {
        JsonWebToken CreateJsonWebToken(User user);
    }
}
