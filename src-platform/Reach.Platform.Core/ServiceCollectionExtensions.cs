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
        services.AddScoped<IEditorService, HttpEditorService>();
        services.AddScoped<IEditorDefinitionService, HttpEditorDefinitionService>();
        services.AddScoped<IFieldDefinitionService, HttpFieldDefinitionService>();
        services.AddScoped<IComponentDefinitionService, HttpComponentDefinitionService>();
        services.AddScoped<IComponentService, HttpComponentService>();

        return services;
    }
    
}