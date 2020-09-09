using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtApiTest.Application.Interfaces;
using JwtApiTest.Domain.BaseResponse;
using Microsoft.AspNetCore.Mvc;

namespace JwtApiTest.Controllers
{
    [Route("api/[controller]")]

    public class UserController : Controller
    {
        private readonly IAuthenticateService _authService;
        private readonly IUserService _userService;

        public UserController(IAuthenticateService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpGet("{email}"), Route("profile")]
        public async Task<ICommandResult> Get(string email)
        {
            var response = await _userService.GetUserByEmail(email);
            return response;
        }
    }
}
