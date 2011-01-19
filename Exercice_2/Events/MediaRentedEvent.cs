using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Eventing.Sourcing;

namespace Events
{
    [Serializable]
    public class MediaRentedEvent : SourcedEvent
    {
        public readonly Guid MediaId;
        public readonly DateTime DueDate;
        public MediaRentedEvent(Guid mediaId, DateTime dueDate)
        {
            this.MediaId = mediaId;
            this.DueDate = dueDate;
        }
    }
}
