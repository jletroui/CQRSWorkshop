using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class RentedMedia
    {
        public readonly Guid MediaId;
        public readonly DateTime DueDate;
        public bool IsLateForReturn
        {
            get
            {
                return DueDate < DateTime.Today;
            }
        }
        public RentedMedia(Guid mediaId, DateTime dueDate)
        {
            this.MediaId = mediaId;
            this.DueDate = dueDate;
        }

    }
}
