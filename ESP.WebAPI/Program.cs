
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
public class Program
{
    public static void Main(string[] args)
    {
        Env.Load();
        var builder = WebApplication.CreateBuilder(args);

        
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

        //Permet de valider les tokens JWT reçus

        var jwtKey = builder.Configuration["Jwt:Key"]!;
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtKey))
                };
            });

        builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

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
        builder.Services.AddSwaggerGen(c =>
        {
            //aide de l'ia pour cette partie
            c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "Entrez votre token JWT ici"
            });
            c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Reference = new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("AllowReact");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}