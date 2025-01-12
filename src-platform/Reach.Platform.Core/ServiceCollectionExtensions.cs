using Microsoft.Extensions.DependencyInjection;
using Reach.Platform.ServiceModel;
using Reach.Platform.Services;

namespace Reach.Platform;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPlatformServices(this IServiceCollection services)
    {
        services.AddScoped<IGraphClient, DefaultGraphClient>();
        services.AddScoped<ICommandClient, DefaultCommandClient>();
        
        services.AddScoped<IGraphQueryBuilder, DefaultGraphQueryBuilder>();
        
        services.AddScoped<IMembershipService, HttpMembershipService>();
        services.AddScoped<IOrganizationService, HttpOrganizationService>();
        
        return services;
    }
    
}