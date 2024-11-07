using System.Reflection;

namespace Reach.Silo.Host.Extensions;

public class GrainEndpointConfig<TGrain>
{
    public GrainEndpointConfig<TGrain> Add(Type commandType, string methodName)
    {
        var methodInfo = typeof(TGrain).GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);

        Maps.Add(new GrainEndpointCommandMap<TGrain>
        {
            CommandType = commandType,
            Method = methodInfo
        });

        return this;
    }

    public GrainEndpointConfig<TGrain> Add(Type commandType, MethodInfo method)
    {
        Maps.Add(new GrainEndpointCommandMap<TGrain>
        {
            CommandType = commandType,
            Method = method
        });

        return this;
    }

    public List<GrainEndpointCommandMap<TGrain>> Maps { get; init; } = new();
}
