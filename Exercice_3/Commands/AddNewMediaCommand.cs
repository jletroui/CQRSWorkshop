using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding;

namespace Commands
{
    public class AddNewMediaCommand : CommandBase
    {
        public readonly Guid MediaId;
        public readonly string Title;

        public AddNewMediaCommand(Guid mediaId, string title)
        {
            MediaId = mediaId;
            Title = title;
        }    
    }
}
