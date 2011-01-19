using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding.CommandExecution;
using Commands;
using Ncqrs.Domain;
using Domain;

namespace CommandHandlers
{

    public class CustomerRentMediaCommandHandler : CommandExecutorBase<CustomerRentMediaCommand>
    {
        protected override void ExecuteInContext(IUnitOfWorkContext context, CustomerRentMediaCommand command)
        {
            var customer = context.GetById<Customer>(command.CustomerId);
            var media = context.GetById<Media>(command.MediaId);

            customer.Rent(media);

            context.Accept();
        }
    }
}
