using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group32_API.DTOs
{
    public class Review4CreationOrUpdateDto
    {
        public string Email { get; set; } // persons full name
        public string DateTime { get; set; }
        public int Rating { get; set; } // # 1-10
        public string Comment { get; set; }
    }
}
