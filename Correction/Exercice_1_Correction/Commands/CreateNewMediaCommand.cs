using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding;

namespace Commands
{
    public class CreateNewMediaCommand : CommandBase
    {
        public readonly Guid MediaId;
        public readonly string Title;

        public CreateNewMediaCommand(Guid mediaId, string title)
        {
            MediaId = mediaId;
            Title = title;
        }    
    }
}
