using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group32_API.Models
{
    public interface IDestinationRepository
    {
        Task<bool> DestinationExists(int desId);
        Task<IEnumerable<DestinationInfo>> GetListDestinations();
        Task<IEnumerable<DestinationInfo>> GetAllRestaurant(string category="Restaurant");
        Task<DestinationInfo> GetDestinationById(int desId, bool includeReviews);
        Task<Review> GetReviewById(int reviewId);
        Task<IEnumerable<Review>> GetReviewsForDestination(int desId);
        Task<IEnumerable<Review>> GetReviewsByRatingForDetination(int desId, int rating);
        //create
        void CreateDestination(DestinationInfo des);
        Task AddReviewForDestination(int desId, Review newReview);
        //update
        //Task UpdateAReviewForDestination(int desId, Review updateReview);
        //Task UpdateDestination(int desId, Destination destination);
        //delete
        Task DeleteDestination(int desId);
        Task DeleteReview(int reviewId);
        Task<bool> Save();
    }
}
