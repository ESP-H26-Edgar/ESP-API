using ESP.Domain.Interfaces.Repositories;
using ESP.Domain.Interfaces.Security;
using ESP.Infrastructure.Repositories;
using ESP.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ESP.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            ).LogTo(msg => System.IO.File.AppendAllText("ef_log.txt", msg + "\n"),
             Microsoft.Extensions.Logging.LogLevel.Information)
            .EnableSensitiveDataLogging());

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IStripeService, StripeService>();
        return services;
    }
}