using Application.Contracts.Infrastructure;
using Application.Contracts.Infrastructure.ExternalServices;
using Infrastructure.ExternalServices;
using Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.FileUpload;

namespace Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        Console.Write("section" + CloudinaryUrl.SectionName);
        services.Configure<CloudinaryUrl>(configuration.GetSection(CloudinaryUrl.SectionName));
        
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddSingleton<IConfiguration>(configuration);
        services.AddScoped<ICodeforcesApiService, CodeforcesApiService>();
        services.AddScoped<IFileUpload, FileUploader>();
        services.Configure<CodeforcesAPISettings>(
            configuration.GetSection("CodeforcesAPISettings")
        );
        services.AddHttpClient();

        return services;
    }
}
