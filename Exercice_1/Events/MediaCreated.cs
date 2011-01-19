using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ncqrs.Eventing.Sourcing;

namespace Events
{
    [Serializable]
    public class MediaCreated : SourcedEvent
    {
        public MediaCreated(string title)
        {
            Title = title;
        }
        public readonly string Title;
    }
}
