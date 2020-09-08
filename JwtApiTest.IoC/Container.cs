using JwtApiTest.Application.Interfaces;
using JwtApiTest.Application.Interfaces.IRepository;
using JwtApiTest.Application.ServicesLogic;
using JwtApiTest.Application.Utils;
using JwtApiTest.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtApiTest.IoC
{
    public static class Container
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddRepositories();
            services.AddServices();

            return services;
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<InMemoryDatabaseContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<JwtSettings>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddTransient<IAuthenticateService, AuthenticateService>();
            services.AddTransient<IUserService, UserService>();

        }
    }
}
