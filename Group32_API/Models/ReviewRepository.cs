using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group32_API.Models
{
    public class ReviewRepository : IReviewRepository // for implementation refer ppt: week#9(API with EFCORE) --> slide # 10
    {
        private ReviewDBContext _context;

        public ReviewRepository(ReviewDBContext context)
        {
            _context = context;
        }
    }
}
