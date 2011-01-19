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
    public class CreateNewMediaCommandHandler : CommandExecutorBase<CreateNewMediaCommand>
    {
        protected override void ExecuteInContext(IUnitOfWorkContext context, CreateNewMediaCommand command)
        {
            var media = new Media(command.MediaId, command.Title);

            context.Accept();
        }
    }
}
