using System;
using System.Collections.Generic;
using System.Text;

namespace Playing
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
