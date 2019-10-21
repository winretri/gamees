using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NeverCompletedGame.Infrastructure;
using Playing;

namespace Infrastructure
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICommandHandler<T> GetCommandHandler<T>(string domain,
            string aggregate,
            string command) where T : ICommand
        {
            Assembly domainAssembly = FactoryHelper.GetDomainAssembly(domain);
            if (domainAssembly == null)
            {
                throw new Exception($"Unknown domain {domain}.");
            }

            Type aggregateType = FactoryHelper.GetAggregateType(domainAssembly, aggregate);

            if (aggregateType == null)
            {
                throw new Exception($"Unknown aggregateType {aggregate} in domain {domain}.");
            }

            Type commandType = FactoryHelper.GetCommandType(domainAssembly, aggregateType, command);

            if (commandType == null)
            {
                throw new Exception($"Unknown commandType {command} in domain {domain} on aggregate {aggregate}.");
            }

            
            Type commandHandlerType = GetCommandHandlerType(commandType);
            return (ICommandHandler<T>)_serviceProvider.GetService(commandHandlerType);
        }

        
        private static Type GetCommandHandlerType(Type commandType)
        {
            Type commandHandler = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).FirstOrDefault(t => t.GetInterfaces().Any(interfaceType => interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(ICommandHandler<>) && interfaceType.GenericTypeArguments.Any(git => git == commandType)));
            return commandHandler;
        }

    }
}
