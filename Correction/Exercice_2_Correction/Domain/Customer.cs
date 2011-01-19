using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Domain;
using Events;

namespace Domain
{
    public class Customer : AggregateRootMappedByConvention
    {
        private class Renting
        {
            public readonly Guid MediaId;
            public readonly DateTime DueDate;

            public Renting(Guid mediaId, DateTime rentingDate)
            {
                MediaId = mediaId;
                DueDate = rentingDate.AddDays(7);
            }
        }

        private List<Renting> rentings = new List<Renting>();

        private Customer() { }

        public Customer(Guid customerId, String name)
            : base(customerId)
        {
            ApplyEvent(new NewCustomerRegisteredEvent(name));
        }

        public void Rent(Media media)
        {
            if (rentings.Count >= 3)
            {
                throw new BusinessRuleViolatedException(ErrorCode.MaximumOfMediaPermitedExceeded);
            }
            if (rentings.Any(x => x.DueDate < DateTime.Now))
            {
                throw new BusinessRuleViolatedException(ErrorCode.CanNotRentWhenLateReturn);
            }
            if (rentings.Any(x => x.MediaId == media.EventSourceId))
            {
                throw new BusinessRuleViolatedException(ErrorCode.AlreadyRented);
            }

            ApplyEvent(new CustomerRentedMediaEvent(media.EventSourceId, DateTime.Now.AddDays(7)));
        }

        public void Return(Guid mediaId)
        {
            var renting = rentings.SingleOrDefault(x => x.MediaId == mediaId);

            if (renting == null)
            {
                throw new BusinessRuleViolatedException(ErrorCode.MediaIsNotRentedByThisCustomer);
            }

            ApplyEvent(new CustomerReturnedMediaEvent(mediaId));
        }

        protected void OnNewCustomerCreatedEvent(NewCustomerRegisteredEvent e)
        {
        }

        protected void OnCustomerRentedMediaEvent(CustomerRentedMediaEvent evt)
        {
            rentings.Add(new Renting(evt.MediaId, DateTime.Now));
        }

        protected void OnCustomerReturnedMediaEvent(CustomerReturnedMediaEvent evt)
        {
            rentings.Remove(rentings.Single(x => x.MediaId == evt.MediaId));
        }
    }
}
