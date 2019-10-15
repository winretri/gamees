using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IVersionedEvent
    {
        Guid SourceId { get; set; }

        int Version { get; set; }
    }
}
