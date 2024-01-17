using Application.Contracts.Persistence;
using Application.Contracts.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories.Common;
using Persistence.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            var connectionString = webHostEnvironment.IsDevelopment()
                ? configuration["Local:Connection_String"]
                : Environment.GetEnvironmentVariable("Render_Internal_Connection_String");
            
            services.AddDbContext<AppDBContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IContestRepository, ContestRepository>();
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