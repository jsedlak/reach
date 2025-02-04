using Microsoft.Extensions.DependencyInjection;

namespace Reach.Silo.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection UseDirectEventCommunication(this IServiceCollection services) 
    {
        services.Configure<ViewManagementOptions>(options => options.UseDirectCommunication = true);
        return services;
    }
}