using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group32_API.Models
{
    public class DestinationRepository : IDestinationRepository // for implementation refer ppt: week#9(API with EFCORE) --> slide # 10
    {
        private DestinationDBContext _context;

        public DestinationRepository(DestinationDBContext context)
        {
            _context = context;
        }
    }
}
