using System.Reflection;

namespace Reach.Silo.Host.Extensions;

public class GrainEndpointConfig<TGrain>
{
    public GrainEndpointConfig<TGrain> Add(Type commandType, string methodName)
    {
        var methodInfo = typeof(TGrain).GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);

        if(methodInfo is null)
        {
            throw new InvalidOperationException($"Cannot find the method {methodName} on {commandType.FullName}");
        }

        Maps.Add(new GrainEndpointCommandMap<TGrain>
        {
            CommandType = commandType,
            Method = methodInfo
        });

        Console.WriteLine($"Added Grain Map for {commandType.Name}");

        return this;
    }

    public GrainEndpointConfig<TGrain> Add(Type commandType, MethodInfo method)
    {
        Maps.Add(new GrainEndpointCommandMap<TGrain>
        {
            CommandType = commandType,
            Method = method
        });

        Console.WriteLine($"Added Grain Map for {commandType.Name}");

        return this;
    }

    public List<GrainEndpointCommandMap<TGrain>> Maps { get; init; } = new();
}
