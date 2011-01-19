using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding;

namespace Commands
{
    public class CustomerRentMediaCommand : CommandBase
    {
        public readonly Guid CustomerId;
        public readonly Guid MediaId;

        public CustomerRentMediaCommand(Guid customerId, Guid mediaId)
        {
            CustomerId = customerId;
            MediaId = mediaId;
        }
    }
}
