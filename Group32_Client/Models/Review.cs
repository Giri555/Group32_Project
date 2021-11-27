using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Group32_Client.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int DestinationId { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public string Email { get; set; }
        public string DateTime { get; set; }
    }
}
