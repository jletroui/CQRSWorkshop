using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commands;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Domain;
using Domain;

namespace CommandHandlers
{
    public class RegisterNewCustomerCommandHandler : CommandExecutorBase<RegisterNewCustomerCommand>
    {
        protected override void ExecuteInContext(IUnitOfWorkContext context, RegisterNewCustomerCommand command)
        {
            var customer = new Customer(command.CustomerId, command.Name);

            context.Accept();
        }
    }
}
