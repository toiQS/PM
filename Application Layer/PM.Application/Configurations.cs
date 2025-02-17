﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PM.Application.Implements;
using PM.Application.Interfaces;
using PM.Infrastructers;
using PM.Persistence;

namespace PM.Application
{
    public static class Configurations
    {
        public static void IntializeApplication(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterServices(services, configuration);
            services.InitializePersistence(configuration);
            services.InitializeInfrastructure(configuration);
        }
        private static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthLogic, AuthLogic>();
            services.AddScoped<IUserLogic, UserLogic>();
        }
    }
}
