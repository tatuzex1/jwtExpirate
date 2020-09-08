using JwtApiTest.Application.Interfaces;
using JwtApiTest.Application.Interfaces.IRepository;
using JwtApiTest.Domain.BaseResponse;
using JwtApiTest.Domain.Models;
using JwtApiTest.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static JwtApiTest.Domain.BaseResponse.Response;

namespace JwtApiTest.Application.ServicesLogic
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<ICommandResult> CreateUser(CreateUser request)
        {
            var existsUser = await _repository.ExistsUser(request.Email);

            if (existsUser)
            {
                return new GenericCommandResult(false, "Usuário já registrado no sistema.", null);
            }

            var user = new User(request.Name, request.Email, request.Password);

            foreach (var role in request.Roles)
            {
                user.AddRole(role);
            }

            foreach (var policy in request.Permissions)
            {
                user.AddPermission(policy);
            }

            await _repository.Save(user);

            return new GenericCommandResult(true, "Usuário cadastrado com sucesso.", new
            {
                user.Id,
                user.Name,
                user.Email,
                user.Roles,
                user.Permissions
            });

        }

        public async Task<ICommandResult> GetUserByEmail(string email)
        {
            var existsUser = await _repository.ExistsUser(email);

            if (!existsUser)
            {
                return new GenericCommandResult(false, "Nenhum usuário encontrado com esse email.", null);
            }

           var user = await _repository.Get(email);

            return new GenericCommandResult(true, "Usuário encontrado com sucesso.",user);
        }
    }
}
