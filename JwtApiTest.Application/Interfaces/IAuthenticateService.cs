using JwtApiTest.Domain.BaseResponse;
using JwtApiTest.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JwtApiTest.Application.Interfaces
{
    public interface IAuthenticateService
    {
        Task<ICommandResult> Authenticate(AuthenticateRequest request);
    }
}
