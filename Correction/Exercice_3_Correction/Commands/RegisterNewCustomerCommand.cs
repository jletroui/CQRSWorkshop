using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding;

namespace Commands
{
    public class RegisterNewCustomerCommand : CommandBase
    {
        public readonly Guid CustomerId;
        public readonly string Name;

        public RegisterNewCustomerCommand(Guid customerId, string name)
        {
            CustomerId = customerId;
            Name = name;
        }    
    }
}
