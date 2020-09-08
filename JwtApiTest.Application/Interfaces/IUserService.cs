using JwtApiTest.Domain.BaseResponse;
using JwtApiTest.Domain.Requests;
using System.Threading.Tasks;
using static JwtApiTest.Domain.BaseResponse.Response;

namespace JwtApiTest.Application.Interfaces
{
    public interface IUserService
    {
        Task<ICommandResult> CreateUser(CreateUser request);

        Task<ICommandResult> GetUserByEmail(string email);
    }
}
