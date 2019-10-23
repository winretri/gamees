using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Playing;
using Playing.Commands;

namespace NeverCompletedGame.Infrastructure
{
    internal static class FactoryHelper
    {
        internal static Assembly GetDomainAssembly(string domain)
        {
            Assembly domainAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(ass => ass.GetName().Name.ToLower() == domain?.ToLower());
            return domainAssembly;
        }

        internal static Type GetAggregateType(Assembly domainAssembly, string aggregate)
        {

            Type aggregateType = domainAssembly.GetTypes().FirstOrDefault(t => t.Name.ToLower() == aggregate);
            return aggregateType;
        }

        internal static Type GetCommandType(Assembly domainAssembly, Type aggregateType, string command)
        {
            Type commandType = domainAssembly.GetTypes().FirstOrDefault(t => typeof(ICommand).IsAssignableFrom(t) && GetAggregateAttribute(t) != null && t.Name.ToLower() == command.ToLower());
            return commandType;
        }


        private static AggregateAttribute GetAggregateAttribute(Type t)
        {
            return (AggregateAttribute)Attribute.GetCustomAttribute(t, typeof(AggregateAttribute));
        }

    }
}
