using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Events;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace ReadModel.Denormalizers
{
    public class MediaItemDenormalizer : IEventHandler<NewMediaCreatedEvent>
    {
        public void Handle(NewMediaCreatedEvent evnt)
        {
            using (var context = new ReadModelContainer())
            {
                var newItem = new MediaItem
                {
                    Id = evnt.EventSourceId,
                    Title = evnt.Title,
                    
                };

                context.MediaItems.AddObject(newItem);
                context.SaveChanges();
            }            
        }
    }
}
