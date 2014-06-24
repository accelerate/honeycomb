namespace Honeycomb.Container
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Cell;

    internal class CellReflector : MarshalByRefObject
    {
        public string[] GetReceivers(string assemblyName, string cellTypeFullName)
        {
            Assembly.ReflectionOnlyLoad(typeof (IReceive<>).Assembly.FullName);
            Assembly assembly = Assembly.ReflectionOnlyLoad(assemblyName);
            Type type = assembly.GetType(cellTypeFullName);
            return type.GetInterfaces()
                       .Where(iface => iface.IsGenericType)
                //Types are not directly equal, possibly due to being loaded in multiple app domains, so using Guid check.
                       .Where(iface => typeof (IReceive<>).GUID == iface.GetGenericTypeDefinition().GUID)
                       .Select(iface => iface.GetGenericArguments()[0].FullName)
                       .ToArray();
        }
    }
}