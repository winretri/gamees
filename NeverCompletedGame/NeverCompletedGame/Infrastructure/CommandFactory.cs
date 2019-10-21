using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Playing;
using System.Text.Json;

namespace NeverCompletedGame.Infrastructure
{
    public class CommandFactory : ICommandFactory
    {
        public T GetCommand<T>(string domain, string aggregate, string command, string payload)
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

            if (string.IsNullOrEmpty(payload))
            {
                payload = "{}";
            }

            T commandObject = (T)JsonSerializer.Deserialize(payload, commandType, new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});
            return commandObject;
        }
    }
}
