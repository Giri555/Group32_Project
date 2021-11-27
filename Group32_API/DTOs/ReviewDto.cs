using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group32_API.Models
{
    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public int DestinationId { get; set; } 
        public string Email { get; set; }
        public string DateTime { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
