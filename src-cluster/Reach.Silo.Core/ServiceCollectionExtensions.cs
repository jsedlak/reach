using Microsoft.Extensions.DependencyInjection;
using Reach.Silo.Migrations;

namespace Reach.Silo;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMigrations(this IServiceCollection services)
    {
        services.AddScoped<IMigrationService, MigrationService>();

        return services;
    }
}
