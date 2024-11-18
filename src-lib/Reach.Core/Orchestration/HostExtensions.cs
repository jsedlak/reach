using Microsoft.Extensions.DependencyInjection;
using Reach.Orchestration.Model;
using Reach.Orchestration.ServiceModel;
using Reach.Orchestration.Services;

namespace Reach.Orchestration;

public static class HostExtensions
{
    public static IServiceCollection WithInMemoryRegions(this IServiceCollection services, params Region[] regions)
    {
        return services.AddScoped<IRegionProvider, InMemoryRegionProvider>(sp =>
        {
            var formatter = sp.GetRequiredService<IRegionUrlFormatter>();
            return new InMemoryRegionProvider(formatter, regions);
        });
    }

    public static IServiceCollection WithDelegatedRegionUrls(
        this IServiceCollection services,
        Func<Region, string> apiBaseUrlCallback,
        Func<Region, string> graphBaseUrlCallback)
    {
        return services.AddScoped<IRegionUrlFormatter, DelegatingRegionUrlFormatter>(sp =>
            new DelegatingRegionUrlFormatter(apiBaseUrlCallback, graphBaseUrlCallback)
        );
    }

    public static IServiceCollection WithPathRegionUrls(
        this IServiceCollection services,
        string baseUrlFormat,
        string apiRoute = "/v1/api/",
        string graphRoute = "/v1/graph/")
    {
        return services.AddScoped<IRegionUrlFormatter, PathBasedRegionUrlFormatter>(sp =>
            new PathBasedRegionUrlFormatter(baseUrlFormat, apiRoute, graphRoute));
    }
}