using Group32_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group32_API.Services
{
    interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetReviewsForDestination(int desId);
        Task<IEnumerable<Review>> GetReviewsByRatingForDetination(int desId, int rating);
        
        Task AddReviewForDestination(int desId, Review newReview);
        //update
        //Task UpdateAReviewForDestination(int desId, Review updateReview);
        //delete
        void DeleteReview(int desId, int reviewId);
        Task<bool> Save();
    }
}
