using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group32_API.Models
{
    public class Destination // ####### this should be partial class?
    {
        public int DestinationId { get; set; } // PK
        public string Name { get; set; } // name of the destination
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Type { get; set; } // restaurant, hotel, park, attraction, etc,... what type of destination is it?

/*
        public Destination()
        {

        }

        public Destination(int destinationId, string name, string address, string phoneNumber, string type)
        {
            DestinationId = destinationId;
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Type = type;
        }
*/

    }
}
