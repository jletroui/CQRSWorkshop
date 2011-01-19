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
    public class AddNewMediaCommandHandler : CommandExecutorBase<AddNewMediaCommand>
    {
        protected override void ExecuteInContext(IUnitOfWorkContext context, AddNewMediaCommand command)
        {
            var media = new Media(command.MediaId, command.Title);

            context.Accept();
        }
    }
}
