using System;
using System.Collections.Generic;

#nullable disable

namespace Group32_API.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public string Email { get; set; }
        public string DateTime { get; set; }
        public int DestinationId { get; set; }

        public virtual DestinationInfo Destination { get; set; }
    }
}
