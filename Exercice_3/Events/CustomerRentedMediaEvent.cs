using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Eventing.Sourcing;

namespace Events
{
    public class CustomerRentedMediaEvent : SourcedEvent
    {
        public readonly Guid MediaId;
        public readonly DateTime DueDate;

        public CustomerRentedMediaEvent(Guid mediaId, DateTime dueDate)
        {
            MediaId = mediaId;
            DueDate = dueDate;
        }

    }
}
