using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Eventing.Sourcing;

namespace Events
{
    [Serializable]
    public class NewMediaCreatedEvent : SourcedEvent
    {
        public readonly String Title;

        public NewMediaCreatedEvent(string title)
        {
            Title = title;
        }
    }
}
