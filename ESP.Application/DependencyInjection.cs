
using ESP.Application.UseCases;
using ESP.Application.Validators;
using ESP.Domain.Interfaces.Repositories;
using ESP.Domain.Interfaces.Security;
using ESP.Infrastructure.Repositories;
using ESP.Infrastructure.Security;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ESP.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // cette ligne ajoute les validators
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<LoginUseCase>();
        services.AddScoped<IUserRepository, UserRepository>();


        services.AddValidatorsFromAssemblyContaining<LoginValidation>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();



        services.AddScoped<IJwtTokenService, JwtTokenService>();

        return services;
    }
}