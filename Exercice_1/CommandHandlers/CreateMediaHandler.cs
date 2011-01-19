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
    public class CreateMediaHandler : CommandExecutorBase<CreateMedia>
    {
        protected override void ExecuteInContext(IUnitOfWorkContext context, CreateMedia command)
        {
            var media = new Media(command.Id, command.Title);
            context.Accept();
        }
    }
}
