﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group32_Client.Models
{
    public class Restaurant
    {
        public int DestinationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        //public virtual ICollection<Review> Reviews { get; set; }
    }
}
