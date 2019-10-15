using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Infrastructure
{
    public class EventStoreEvent
    {

        public virtual string EventContainer { get; set; }

        [Key]
        public string Id { get; internal set; }

        public string DomainEventName { get; set; }

        public string AggregateId { get; internal set; }


    }
}
