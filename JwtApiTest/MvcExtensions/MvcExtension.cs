﻿using JwtApiTest.Application.Utils;
using JwtApiTest.Domain.Infracstruture;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace JwtApiTest.MvcExtensions
{
    public static class MvcExtension
    {
        public static void AddAuthorizedMvc(this IServiceCollection services)
        {
            AddJwtAuthorization(services);
            AddAuthenticatedUser(services);
            AddMvc(services);
        }

        private static void AddJwtAuthorization(IServiceCollection services)
        {
            var jwtSettings = services.BuildServiceProvider().GetRequiredService<JwtSettings>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateActor = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = jwtSettings.SigningCredentials.Key
                    };
                });


        }

        private static void AddAuthenticatedUser(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<AuthenticatedUser>();
        }

        private static void AddMvc(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                config.Filters.Add(new AuthorizeFilter(policy));               
            });

        }
    }
}
