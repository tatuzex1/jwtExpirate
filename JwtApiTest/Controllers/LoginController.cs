using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtApiTest.Application.Interfaces;
using JwtApiTest.Domain.BaseResponse;
using JwtApiTest.Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JwtApiTest.Domain.BaseResponse.Response;

namespace JwtApiTest.Controllers
{
    [Route("api/[controller]")]

    public class LoginController : Controller
    {

        private readonly IAuthenticateService _authService;
        private readonly IUserService _userService;

        public LoginController(IUserService userService, IAuthenticateService authService)
        {
            _authService = authService;
            _userService = userService;
        }
        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        /// <param name="command">Informações do usuário</param>
        /// <returns>Informações básicas do usuário recém criado</returns>
        [HttpPost, AllowAnonymous, Route("register")]
        public async Task<ICommandResult> CreateUser([FromBody] CreateUser request)
        {
            var genericResult =  await _userService.CreateUser(request);
            return genericResult;
        }

        [HttpPost, AllowAnonymous, Route("login")]
        public async Task<ICommandResult> Authenticate([FromBody] AuthenticateRequest request)
        {
            var genericResult = await _authService.Authenticate(request);
            return genericResult;
        }

        [HttpGet("{email}"), Route("profile")]
        public async Task<ICommandResult> Get(string email)
        {
            var response = await _userService.GetUserByEmail(email);
            return response;
        }
    }
}