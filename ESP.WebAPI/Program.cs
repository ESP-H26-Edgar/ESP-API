
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using ESP.Infrastructure;
using ESP.Infrastructure.Repositories;
using ESP.Application.UseCases;
using ESP.Domain.Interfaces.Repositories;
using ESP.Domain.Interfaces.Security;
using ESP.Application.Validators;
using Microsoft.AspNetCore.Identity;
using ESP.Infrastructure.Security;
using ESP.Application.DTOS;
using DotNetEnv;
public class Program
{
    public static void Main(string[] args)
    {
        Env.Load();
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();


        builder.Services.AddScoped<LoginUseCase>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        var connectionTemplate = builder.Configuration.GetConnectionString("DefaultConnection");

        var connectionString = connectionTemplate
            .Replace("%DB_HOST%", Environment.GetEnvironmentVariable("DB_HOST")!)
            .Replace("%DB_PORT%", Environment.GetEnvironmentVariable("DB_PORT")!)
            .Replace("%DB_NAME%", Environment.GetEnvironmentVariable("DB_NAME")!)
            .Replace("%DB_USER%", Environment.GetEnvironmentVariable("DB_USER")!)
            .Replace("%DB_PASSWORD%", Environment.GetEnvironmentVariable("DB_PASSWORD")!);

        builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);
        ;

        builder.Services.AddValidatorsFromAssemblyContaining<LoginValidation>();

        builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();


        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowReact",
                policy => policy
                    .WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });

        

        // Add Swagger/OpenAPI
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseCors("AllowReact");
        app.MapControllers();

        app.Run();
    }
}