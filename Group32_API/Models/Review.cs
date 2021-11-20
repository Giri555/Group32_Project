using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group32_API.Models
{
    public class Review // ####### this should be partial class?
    {
        public int ReviewId { get; set; } // PK
        public int DestinationId { get; set; } // FK
        public string Name { get; set; } // persons full name
        public string DateTime { get; set; }
        public int Rating { get; set; } // # 1-10
        public string Comment { get; set; }

/*
        public Review()
        {

        }

        public Review(int reviewId, int destinationId, string name, string dateTime, int rating, string comment)
        {
            ReviewId = reviewId;
            DestinationId = destinationId;
            Name = name;
            DateTime = dateTime;
            Rating = rating;
            Comment = comment;
        }
*/

    }
}
