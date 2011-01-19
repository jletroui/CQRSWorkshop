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
        private Media()
        { 
        
        }

        public Media(Guid id, string title) 
            : base(id)
        {
            ApplyEvent(new MediaCreated(title));      
        }

        protected void OnMediaCreated(MediaCreated creationEvent)
        { 
            
        }
    }
}
