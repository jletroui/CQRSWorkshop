using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Domain;
using Events;

namespace Domain
{
    public class Media : AggregateRootMappedByConvention
    {
        private Media() { }

        public Media(Guid mediaId, String title) : base(mediaId)
        {
            ApplyEvent(new NewMediaCreatedEvent(title));
        }

        protected void OnNewMediaCreatedEvent(NewMediaCreatedEvent e)
        {
        }    
    }
}
