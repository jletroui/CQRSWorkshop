using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Events;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace ReadModel.Denormalizers
{
    public class CutomerRentedItemDenormalizer : IEventHandler<MediaRentedEvent>
    {
        public void Handle(MediaRentedEvent evnt)
        {
            using (var context = new ReadModelContainer())
            {
                var mediaItem = context.MediaItems.Single(x => x.Id == evnt.MediaId);
                var customerItem = context.CustomerItems.Single(x => x.Id == evnt.EventSourceId);
                var newItem = new CustomerRentedItem
                {
                    CustomerId = customerItem.Id,
                    MediaId= mediaItem.Id,
                    CustomerName=customerItem.Name,
                    MediaTitle=mediaItem.Title,
                    DueDate= evnt.DueDate,
                };

                context.CustomerRentedItems.AddObject(newItem);
                context.SaveChanges();
            }
        }
    }
}
