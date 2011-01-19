using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Eventing.Sourcing;

namespace Events
{
    [Serializable]
    public class NewCustomerRegisteredEvent : SourcedEvent
    {
        public readonly String Name;

        public NewCustomerRegisteredEvent(string name)
        {
            Name = name;
        }
    }
}
