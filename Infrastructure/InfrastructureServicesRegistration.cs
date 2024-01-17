using Application.Contracts.Infrastructure;
using Application.Contracts.Infrastructure.ExternalServices;
using Infrastructure.ExternalServices;
using Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.FileUpload;
using Infrastructure.JwtAuthentication;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
        services.AddHttpContextAccessor();
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddScoped<IAuthenticationService,AuthenticationService>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddSingleton<IConfiguration>(configuration);
        services.AddScoped<IFetchedDataProcessing, FetchedDataProcessing>();
        services.AddScoped<ICurrentLoggedInService, CurrentLoggedInService>();
        services.AddScoped<IFileUpload, FileUploader>();
        services.Configure<CodeforcesAPISettings>(
            configuration.GetSection("CodeforcesAPISettings")
        );
        services.AddHttpClient();

        // Add JWT authentication
        var key = configuration["JwtSettings:Key"]!;

        services.AddAuthentication(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };
        });

        //Add authorization 
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            options.AddPolicy("Student", policy => policy.RequireRole("Student"));
            options.AddPolicy("Head", policy => policy.RequireRole("Head"));
        });


        return services;
    }
}

