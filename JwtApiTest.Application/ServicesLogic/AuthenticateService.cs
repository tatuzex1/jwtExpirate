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
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        private GenericCommandResult _response;

        public AuthenticateService(IJwtService jwtService, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<ICommandResult> Authenticate(AuthenticateRequest request)
        {
            _response = new GenericCommandResult();

            User user = null;

            if (request.GrantType.Equals("password"))
            {
                user = await UserAuthentication(request);
            }
            else if (request.GrantType.Equals("refresh_token"))
            {
                user = await RefreshToken(request);
            }

            if (!_response.Success)
                return _response;

            await GenerateJwt(user);

            return _response;
        }

        private async Task GenerateJwt(User user)
        {
           var jsonWebToken = _jwtService.CreateJsonWebToken(user);
            await _refreshTokenRepository.Save(jsonWebToken.RefreshToken);
            _response.Data = jsonWebToken;
        }

        private async Task<User> RefreshToken(AuthenticateRequest request)
        {
            var token = await _refreshTokenRepository.Get(request.RefreshToken);

            if (token == null)
            {
                _response.Message = "Refresh token inválido";
                _response.Success = false;

            }
            else if (token.ExpirationDate < DateTime.Now)
            {
                _response.Message = "Refresh Token expirado";
                _response.Success = false;

            }

            return await _userRepository.Get(token.Username);

        }

        private async Task<User> UserAuthentication(AuthenticateRequest request)
        {
            // Encrypt password
            var encodedPassword = new Password(request.Password).Encoded;
            var user = await _userRepository.Authenticate(request.Email, encodedPassword);

            if (user == null)
            {
                _response.Message = "Usuário ou senha inválida.";
                _response.Success = false;
            }
            else
            {
                _response.Success = true;
                _response.Message = "Usuário authenticado com sucesso.";
            }

            return user;
        }
    }
}
