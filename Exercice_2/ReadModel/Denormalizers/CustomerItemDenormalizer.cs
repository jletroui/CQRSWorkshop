using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Events;
using Ncqrs.Eventing.ServiceModel.Bus;
using System.Data.Objects;
using System.Data.SqlClient;

namespace ReadModel.Denormalizers
{
    public class CustomerItemDenormalizer : IEventHandler<NewCustomerRegisteredEvent>
    {
        public void Handle(NewCustomerRegisteredEvent evnt)
        {
            using (var context = new ReadModelContainer())
            {
                var newItem = new CustomerItem
                {
                    Id = evnt.EventSourceId,
                    Name = evnt.Name,
                    CanRent = true,
                    CanReturn = false
                };

                context.CustomerItems.AddObject(newItem);
                context.SaveChanges();
            }            
        }
    }
}
