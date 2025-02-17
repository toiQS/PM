﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PM.Infrastructers.Implements;
using PM.Infrastructers.Interfaces;
using System.ComponentModel.Design;

namespace PM.Infrastructers
{
    public static class Configurations
    {
        public static void InitializeInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterJwt(services, configuration);
            RegisterServices(services);
        }
        private static void RegisterJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration.GetSection("Jwt:Issuer").Value,
                    ValidAudience = configuration.GetSection("Jwt:Audience").Value,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:SecretKey").Value))
                };
            });

        }
        private static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtServices, JwtServices>();
        }
    }
}
