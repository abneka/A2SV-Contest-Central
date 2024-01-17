using Application;
using Persistence;
using Infrastructure;
using Microsoft.OpenApi.Models;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration, builder.Environment);
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.AddControllers();


builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAnyOrigin", policy => { policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "JWT Authorization header using the Bearer scheme.",
        Type = SecuritySchemeType.Http,
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


app.Run();