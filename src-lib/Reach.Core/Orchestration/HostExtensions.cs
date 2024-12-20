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

    public static IServiceCollection WithPathRegionUrls(
        this IServiceCollection services,
        string baseUrlFormat,
        string baseUrlRegionFormat,
        string apiRoute = "/v1/api/",
        string graphRoute = "/v1/graph/")
    {
        return services.AddScoped<IRegionUrlFormatter, PathBasedRegionUrlFormatter>(sp =>
            new PathBasedRegionUrlFormatter(baseUrlFormat, baseUrlRegionFormat, apiRoute, graphRoute));
    }
}