using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding;

namespace Commands
{
    public class CustomerReturnMediaCommand : CommandBase
    {
        public readonly Guid CustomerId;
        public readonly Guid MediaId;

        public CustomerReturnMediaCommand(Guid customerId, Guid mediaId)
        {
            CustomerId = customerId;
            MediaId = mediaId;
        }
    }
}
