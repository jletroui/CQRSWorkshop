using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding;

namespace Commands
{
    public class CreateMedia : CommandBase
    {
        public CreateMedia(Guid id, string title) 
        {
            Id = id;
            Title = title;    
        }
        public readonly Guid Id; 
        public readonly string Title; 

    }
}
