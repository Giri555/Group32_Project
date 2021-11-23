using System;
using System.Collections.Generic;

#nullable disable

namespace Group32_API.Models
{
    public partial class DestinationInfo
    {
        public DestinationInfo()
        {
            Reviews = new HashSet<Review>();
        }

        public int DestinationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
