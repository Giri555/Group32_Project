using Microsoft.EntityFrameworkCore;
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
        public async Task<bool> DestinationExists(int desId)
        {
            return await _context.Destinations.AnyAsync<Destination>(d => d.DestinationId == desId);
        }
        public async Task<IEnumerable<Destination>> GetListDestinations()
        {
            var result = _context.Destinations.OrderBy(d => d.Name);
            return await result.ToListAsync();
        }
        public async Task<Destination> GetDestinationById(int desId, bool includePointsOfInterest)
        {
            IQueryable<Destination> result;
            if (includePointsOfInterest)
            {
                result = _context.Destinations.Include(c => c.Reviews).Where(c => c.DestinationId == desId);
            }
            else
                result = _context.Destinations.Where(c => c.DestinationId == desId);
            return await result.FirstOrDefaultAsync();
        }
        public async Task<Review> GetReviewById(int reviewId)
        {
            IQueryable<Review> result = _context.Reviews.Where(r => r.ReviewId == reviewId);
            return await result.FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Review>> GetReviewsByRatingForDetination(int desId, int rating)
        {
            IQueryable<Review> result = _context.Reviews.Where(r => r.DestinationId == desId && r.Rating == rating);
            return await result.ToListAsync();
        }
        public async Task<IEnumerable<Review>> GetReviewsForDestination(int desId)
        {
            IQueryable<Review> result = _context.Reviews.Where(p => p.DestinationId == desId);
            return await result.ToListAsync();
        }

        public void CreateDestination(Destination des)
        {
            _context.Destinations.Add(des);
        }
        public async Task AddReviewForDestination(int desId, Review review)
        {
            var city = await GetDestinationById(desId, false);
            city.Reviews.Add(review);
        }
        public void DeletePointOfInterest(Review review)
        {
            _context.Reviews.Remove(review);
        }
        public async Task DeleteDestination(int desId)
        {
            var destination = await GetDestinationById(desId, false);
            _context.Destinations.Remove(destination);
        }
        public async Task DeleteReview(int reviewId)
        {
            var review = await GetReviewById(reviewId);
            _context.Reviews.Remove(review);
        }
        public async Task<bool> Save()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
