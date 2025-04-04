﻿namespace Reach.Extensions;

public static class TypeExtensions
{
    public static bool Implements<TInterface>(this Type type)
    {
        return Implements(type, typeof(TInterface));
    }

    public static bool Implements(this Type type, Type interfaceType)
    {
        if (!interfaceType.IsInterface)
        {
            return true;
        }

        return type.GetInterfaces().Any(i => i == interfaceType);
    }
    public static bool IsEnumerableType(this Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>);
    }

    public static Type GetEnumerableType(this Type type)
    {
        if (type == null)
            throw new ArgumentNullException("type");

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            return type.GetGenericArguments()[0];

        var iface = (from i in type.GetInterfaces()
                     where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                     select i).FirstOrDefault();

        if (iface == null)
            throw new ArgumentException("Does not represent an enumerable type.", "type");

        return GetEnumerableType(iface);
    }
}
