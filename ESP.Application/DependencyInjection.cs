
using ESP.Application.Services;
using ESP.Application.UseCases;
using ESP.Application.UseCases.Interface;
using ESP.Application.Validators;
using ESP.Domain.Interfaces.Repositories;
using ESP.Domain.Interfaces.Security;
using ESP.Infrastructure.Repositories;
using ESP.Infrastructure.Security;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
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

        services.AddScoped<IRaceRepository, RaceRepository>();
        services.AddScoped<IGetAllRaceUseCase, GetAllRaceUseCase>();
        services.AddScoped<IGetRaceByIdUseCase, GetRaceByIdUseCase>();
        services.AddScoped<ICreateRaceUseCase, CreateRaceUseCase>();
        services.AddScoped<IDeleteRaceUseCase, DeleteRaceUseCase>();

        services.AddScoped<IInscriptionRepository, InscriptionRepository>();
        services.AddScoped<IStripeService, StripeService>();
        services.AddScoped<CreationPayementUseCase>();
        services.AddScoped<InscriptionCourseUseCase>();
        services.AddScoped<GetInscriptionsUseCase>();

        services.AddScoped<IRaceTypeRepository, RaceTypeRepository>();
        services.AddScoped<IGetAllRaceTypeUseCase, GetAllRaceTypeUseCase>();

        services.AddScoped<ImageService>();


        return services;
    }
}