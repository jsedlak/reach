using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reach.Membership.ServiceModel;
using Reach.Membership.Services;

namespace Reach.Membership.AzureTables;

public static class RegistrationExtensions
{
    /// <summary>
    /// Registers Azure Tables based services for handling the membership set of services
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="connectionName"></param>
    /// <returns></returns>
    public static IHostApplicationBuilder AddAzureTablesMembership(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IOrganizationReadRepository, AzureTablesOrganizationRepository>();
        builder.Services.AddScoped<IOrganizationWriteRepository, AzureTablesOrganizationRepository>();
        builder.Services.AddScoped<IOrganizationService, AzureTablesOrganizationService>();

        return builder;
    }
}
