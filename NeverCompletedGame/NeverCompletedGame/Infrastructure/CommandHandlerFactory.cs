using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Playing;

namespace Infrastructure
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        public ICommandHandler<T> GetCommandHandler<T>(string domain,
            string aggregate,
            string command) where T : ICommand
        {
            Assembly domainAssembly = GetDomainAssembly(domain);
            if (domainAssembly == null)
            {
                throw new Exception($"Unknown domain {domain}.");
            }

            Type aggregateType = GetAggregateType(domainAssembly, aggregate);

            if (aggregateType == null)
            {
                throw new Exception($"Unknown aggregateType {aggregate} in domain {domain}.");
            }

            Type commandType = GetCommandType(domainAssembly, aggregateType, command);

            if (commandType == null)
            {
                throw new Exception($"Unknown commandType {command} in domain {domain} on aggregate {aggregate}.");
            }


            Type commandHandlerType = GetCommandHandlerType(commandType);
            return null;
        }

        private static Assembly GetDomainAssembly(string domain)
        {
            Assembly domainAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(ass => ass.GetName().Name.ToLower() == domain?.ToLower());
            return domainAssembly;
        }

        private static Type GetAggregateType(Assembly domainAssembly, string aggregate){

            Type aggregateType = domainAssembly.GetTypes().FirstOrDefault(t => t.Name.ToLower() == aggregate);
            return aggregateType;
        }

        private static Type GetCommandType(Assembly domainAssembly, Type aggregateType, string command)
        {
            Type commandType = domainAssembly.GetTypes().FirstOrDefault(t => typeof(ICommand).IsAssignableFrom(t) && GetAggregateAttribute(t) != null && t.Name.ToLower() == command.ToLower());
            return commandType;
        }

        private static Type GetCommandHandlerType(Type commandType)
        {
            Type commandHandler = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).FirstOrDefault(t => t.GetInterfaces().Any(interfaceType => interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(ICommandHandler<>) && interfaceType.GenericTypeArguments.Any(git => git == commandType)));
            return commandHandler;
        }

        private static AggregateAttribute GetAggregateAttribute(Type t)
        {
            return (AggregateAttribute)Attribute.GetCustomAttribute(t, typeof(AggregateAttribute));
        }
    }
}
