using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group32_Client.Models
{
    public class Restaurant
    {
        public int DestinationId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
