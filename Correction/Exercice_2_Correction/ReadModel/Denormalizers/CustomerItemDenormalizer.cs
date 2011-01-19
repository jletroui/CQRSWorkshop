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
    public class CustomerItemDenormalizer : IEventHandler<NewCustomerRegisteredEvent>,
        IEventHandler<CustomerRentedMediaEvent>,
        IEventHandler<CustomerReturnedMediaEvent>
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

        public void Handle(CustomerRentedMediaEvent evt)
        {
            using (var context = new ReadModelContainer())
            {
                context.ExecuteStoreCommand("INSERT INTO CustomerRentedItems VALUES (@mediaId, @custId, (SELECT Name FROM CustomerItems WHERE Id=@custId),(SELECT Title FROM MediaItems WHERE Id=@mediaId), @dueDate); UPDATE CustomerItems SET CanRent = (CASE WHEN (SELECT COUNT(MediaId) FROM CustomerRentedItems WHERE CustomerId=@custId) > 2 THEN 0 ELSE 1 END), CanReturn = 1 WHERE Id=@custId",
                    new SqlParameter("@custId", evt.EventSourceId),
                    new SqlParameter("@dueDate", evt.DueDate),
                    new SqlParameter("@mediaId", evt.MediaId));

                context.SaveChanges();
            }
        }

        public void Handle(CustomerReturnedMediaEvent evt)
        {
            using (var context = new ReadModelContainer())
            {
                context.ExecuteStoreCommand("DELETE FROM CustomerRentedItems WHERE CustomerId=@custId AND MediaId=@mediaId; UPDATE CustomerItems SET CanRent = 1, CanReturn = (CASE WHEN (SELECT COUNT(MediaId) FROM CustomerRentedItems WHERE CustomerId=@custId) > 0 THEN 1 ELSE 0 END) WHERE Id=@custId",
                    new SqlParameter("@custId", evt.EventSourceId),
                    new SqlParameter("@mediaId", evt.MediaId));

                context.SaveChanges();
            }
        }
    }
}
