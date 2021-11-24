using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group32_API.Models
{
    public class DestinationRepository : IDestinationRepository
    {
        private DestinationInfoDBContext _context;

        public DestinationRepository(DestinationInfoDBContext context)
        {
            _context = context;
        }
        public async Task<bool> DestinationExists(int desId)
        {
            return await _context.DestinationInfos.AnyAsync<DestinationInfo>(d => d.DestinationId == desId);
        }
        public async Task<IEnumerable<DestinationInfo>> GetListDestinations()
        {
            var result = _context.DestinationInfos.OrderBy(d => d.Name);
            return await result.ToListAsync();
        }
        public async Task<DestinationInfo> GetDestinationById(int desId, bool includePointsOfInterest)
        {
            IQueryable<DestinationInfo> result;
            if (includePointsOfInterest)
            {
                result = _context.DestinationInfos.Include(c => c.Reviews).Where(c => c.DestinationId == desId);
            }
            else
                result = _context.DestinationInfos.Where(c => c.DestinationId == desId);
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

        public void CreateDestination(DestinationInfo des)
        {
            _context.DestinationInfos.Add(des);
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
            _context.DestinationInfos.Remove(destination);
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
