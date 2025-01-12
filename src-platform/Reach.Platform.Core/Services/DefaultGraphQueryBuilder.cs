using System.Reflection;
using System.Text;
using Reach.Extensions;
using Reach.GraphQl;
using Reach.Platform.ServiceModel;

namespace Reach.Platform.Services;

public class DefaultGraphQueryBuilder : IGraphQueryBuilder
{
    public string GetDefaultEntityName<TModel>()
    {
        var attr = typeof(TModel).GetCustomAttribute<GraphQueryNameAttribute>()?.Name;
        return string.IsNullOrWhiteSpace(attr) ? typeof(TModel).Name : attr;
    }
    
    public string BuildBaseQuery<TModel>(string? entityName = null)
    {
        entityName ??= GetDefaultEntityName<TModel>();
        return "query { \r\n" + BuildBaseQuery(entityName, typeof(TModel)) + "\r\n}";
    }

    private string BuildBaseQuery(string entityName, Type entityType)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{entityName}{{");

        var props = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var prop in props)
        {
            if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string))
            {
                var propType = prop.PropertyType;

                if(propType.IsArray)
                {
                    propType = propType.GetElementType();
                }
                else if(propType.IsEnumerableType())
                {
                    propType = propType.GetEnumerableType();
                }

                if (propType is null)
                {
                    continue;
                }

                sb.AppendLine(
                    BuildBaseQuery(prop.Name.ToCamelCase(), propType)
                );
            }
            else
            {
                sb.AppendLine($"{prop.Name.ToCamelCase()}");
            }
        }

        sb.AppendLine("}");

        return sb.ToString();
    }
}