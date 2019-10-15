using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class AggregateAttribute : Attribute
    {

        public AggregateAttribute(Type aggregateType)
        {
            this.AggregateType = aggregateType;
        }

        public Type AggregateType { get; }
    }
}
