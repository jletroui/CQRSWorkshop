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
        private List<RentedMedia> rentedMedia = new List<RentedMedia>();
        private Customer() { }

        public Customer(Guid customerId, String name)
            : base(customerId)
        {
            ApplyEvent(new NewCustomerRegisteredEvent(name));
        }

        public void RentMedia(Media media)
        {
            if (rentedMedia.Any(x=>x.MediaId == media.EventSourceId))
            {
                throw new BusinessRuleViolatedException(ErrorCode.AlreadyRented);
            }
            if (rentedMedia.Count >= 3)
            {
                throw new BusinessRuleViolatedException(ErrorCode.MaximumOfMediaPermitedExceeded);
            }
            if(rentedMedia.Any(x=>x.IsLateForReturn ))
            {
                throw new BusinessRuleViolatedException(ErrorCode.CanNotRentWhenLateReturn);
            }
            ApplyEvent(new MediaRentedEvent(media.EventSourceId, DateTime.Today.AddDays(7) ) );
        }

        protected void OnNewCustomerCreatedEvent(NewCustomerRegisteredEvent e)
        {

        }

        protected void OnMediaRentedEvent(MediaRentedEvent e)
        {
            rentedMedia.Add(new RentedMedia(e.MediaId, e.DueDate));
        }


    }
}
