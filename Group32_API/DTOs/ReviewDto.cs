using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group32_API.Models
{
    public class ReviewDto // for implementation refer ppt: week#9(API with EFCORE) --> slide # 13
    {
        public int ReviewId { get; set; } // PK
        public int DestinationId { get; set; } // FK
        public string Email { get; set; } // persons full name
        public string DateTime { get; set; }
        public int Rating { get; set; } // # 1-10
        public string Comment { get; set; }
    }
}
