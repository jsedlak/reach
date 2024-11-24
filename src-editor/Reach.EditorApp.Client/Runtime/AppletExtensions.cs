using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Reach.Applets;
using System.Reflection;

namespace Reach.EditorApp.Client.Runtime;

public static class AppletExtensions
{
    /// <summary>
    /// Adds all applets to the platform
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebAssemblyHostBuilder AddApplets(this WebAssemblyHostBuilder builder, params Assembly[] assemblies)
    {

        var initializers = new List<IAppletInitializer>();

        foreach (var assembly in assemblies)
        {
            // get all types that implement IAppletInitializer from the assembly and have the custom attribute AppletInitializer
            var types = assembly.GetTypes()
                .Where(t =>
                    t.GetInterfaces().Contains(typeof(IAppletInitializer)) &&
                    t.GetCustomAttribute<AppletInitializerAttribute>() != null
                )
                .DistinctBy(t => t.GetCustomAttribute<AppletInitializerAttribute>()!.Name);

            if (types is null || !types.Any())
            {
                continue;
            }

            foreach (var initializerType in types)
            {
                // TODO: Do we need to check for empty constructor, throw error, or log?
                var initializer = Activator.CreateInstance(initializerType) as IAppletInitializer;
                if (initializer is null)
                {
                    continue;
                }

                // register custom services
                initializer.RegisterClient(builder.Services);

                initializers.Add(initializer);
            }
        }

        return builder;

    }
}
