using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Eventing.Sourcing;
using Commands;
using Ncqrs.Commanding.CommandExecution;
using Domain;

namespace CommandHandlers
{
    public class CustomerRentMediaCommandHandler : CommandExecutorBase<CustomerRentMediaCommand>
    {
        protected override void ExecuteInContext(Ncqrs.Domain.IUnitOfWorkContext context, CustomerRentMediaCommand command)
        {
            var customer = context.GetById<Customer>(command.CustomerId);
            var media = context.GetById<Media>(command.MediaId);
            customer.RentMedia(media);
            context.Accept();
        }
    }
}
