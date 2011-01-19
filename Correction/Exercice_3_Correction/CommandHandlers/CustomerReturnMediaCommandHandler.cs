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
    public class CustomerReturnMediaCommandHandler : CommandExecutorBase<CustomerReturnMediaCommand>
    {
        protected override void ExecuteInContext(IUnitOfWorkContext context, CustomerReturnMediaCommand command)
        {
            var customer = context.GetById<Customer>(command.CustomerId);

            customer.Return(command.MediaId);

            context.Accept();
        }
    }
}
