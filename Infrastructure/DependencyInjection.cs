using Infrastructure.ExternalServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<CodeforcesAPISettings>(configuration.GetSection("CodeforcesAPISettings"));
            return services;
        }
    }
}
