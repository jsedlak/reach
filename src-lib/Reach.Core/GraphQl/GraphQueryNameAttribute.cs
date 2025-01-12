namespace Reach.GraphQl;

[AttributeUsage(AttributeTargets.Class)]
public class GraphQueryNameAttribute : Attribute
{
    public GraphQueryNameAttribute(string name)
    {
        Name = name;
    }
    
    public string Name { get; }
}