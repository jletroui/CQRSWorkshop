using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReadModel;

namespace Website.Models
{
    public class CustomerMoviesModel
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public MediaItem[] Medias { get; set; }
        public CustomerRentedItem[] RentedMedias { get; set; }
    }
}