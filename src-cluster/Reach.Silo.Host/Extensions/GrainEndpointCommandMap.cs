using System.Reflection;

namespace Reach.Silo.Host.Extensions;

public class GrainEndpointCommandMap<TGrain>
{
    public Type CommandType { get; set; } = null!;

    public MethodInfo Method { get; set; } = null!;
}