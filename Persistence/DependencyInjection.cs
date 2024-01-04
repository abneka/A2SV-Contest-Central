using Application.Contracts.Persistence;
using Application.Contracts.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories.Common;
using Persistence.Repositories;
using Application.Contracts.Persistence.Auth;
using Persistence.Repositories.Auth;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // var connectionString = configuration["Render:External_Connection_String"];
            // var connectionString = configuration["Local:Connection_String"];
            var connectionString = Environment.GetEnvironmentVariable("Render_Internal_Connection_String");
            // Console.Out.WriteLine($"Conn String{connectionString}");
            services.AddDbContext<AppDBContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IContestRepository, ContestRepository>();
            services.AddScoped<IAuth, AuthRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ITeamQuestionResultRepository, TeamQuestionResultRepository>();
            services.AddScoped<IUserQuestionResultRepository, UserQuestionResultRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserTypeRepository, UserTypeRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IA2SVGroupRepository, A2SVGroupRepository>();
            services.AddScoped<IUserContestResultRepository, UserContestResultRepository>();
            services.AddScoped<ITeamContestResultRepository, TeamContestResultRepository>();
            services.AddScoped<IContestGroupRepository, ContestGroupRepository>();

            return services;
        }
    }
}