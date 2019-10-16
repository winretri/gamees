using System;
using System.Collections.Generic;
using System.Text;

namespace Playing
{
    public interface IEventSourced
    {
        Guid Id { get; }

        int Version { get; }

        IEnumerable<IVersionedEvent> Events { get; }
    }
}
