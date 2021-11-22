using AutoMapper;
using Group32_API.DTOs;
using Group32_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group32_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private IDestinationRepository _reviewRepository;
        private readonly IMapper _mapper;
        ReviewController(IDestinationRepository destinationRepository, IMapper mapper)
        {
            _reviewRepository = destinationRepository;
            _mapper = mapper;
        }
        //POST create a review 
        [HttpPost("{destinationId}/review")]
        public async Task<ActionResult<ReviewDto>> CreatePointOfInterest(int desId, [FromBody] Review4CreationOrUpdateDto newReview)
        {
            if (newReview == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _reviewRepository.DestinationExists(desId)) return NotFound();

            var review = _mapper.Map<Review>(newReview);

            await _reviewRepository.AddReviewForDestination(desId, review);

            if (!await _reviewRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

           // var createdReview = _mapper.Map<ReviewDto>(review);

            return NoContent();
        }
        // PUT api/<controller>/5

        [HttpPut("{destinationId}/reviews/{id}")]
        public async Task<ActionResult> UpdatePointOfInterest(int desId, int id, [FromBody] Review4CreationOrUpdateDto review)
        {
            if (review == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _reviewRepository.DestinationExists(desId)) return NotFound();

            List<Review> previousReview = await _reviewRepository.GetReviewsForDestination(desId);

            if (previousReview == null) return NotFound();

            _mapper.Map(review, previousReview);


            if (!await _reviewRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{destinationId}/reviews/{reviewId}")]
        public async Task<ActionResult> DeleteReview(int id)
        {
            await _reviewRepository.DeleteReview(id);
            return NoContent();
        }
    }
}
