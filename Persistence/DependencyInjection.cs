using Application.Contracts.Persistence;
using Application.Contracts.Persistence.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence.Repositories.Common;
using Persistence.Repositories;
using Persistence.Repositories.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Persistence.Auth;
using Persistence.Repositories.Auth;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDBContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("A2SV_Contest_Central"));
            });
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuth, AuthRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
