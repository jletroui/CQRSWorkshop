using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Eventing.Sourcing;

namespace Events
{
    public class CustomerReturnedMediaEvent : SourcedEvent
    {
        public readonly Guid MediaId;

        public CustomerReturnedMediaEvent(Guid mediaId)
        {
            MediaId = mediaId;
        }

    }
}
