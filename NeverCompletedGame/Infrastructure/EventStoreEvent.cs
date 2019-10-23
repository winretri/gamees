﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure
{
    public class EventStoreEvent
    {
        #region Public Properties

        public string AggregateId { get; internal set; }

        public string DomainEventName { get; set; }

        public virtual string EventContainer { get; set; }

        public int EventContainerVersion { get; internal set; }

        [Key]
        public string Id { get; internal set; }

        public int Sequence { get; internal set; }

        public DateTime Time { get; internal set; }

        #endregion
    }
}