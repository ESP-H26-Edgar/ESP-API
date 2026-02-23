
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


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();


        builder.Services.AddScoped<LoginUseCase>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
        ));

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