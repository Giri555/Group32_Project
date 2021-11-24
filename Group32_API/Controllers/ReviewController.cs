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
        public ReviewController(IDestinationRepository destinationRepository, IMapper mapper)
        {
            _reviewRepository = destinationRepository;
            _mapper = mapper;
        }
        // GET: api/<controller>
        [HttpGet("{desId}/reviews")]

        public async Task<ActionResult<Review>> GetReviews(int desId)
        {
            if (!(await _reviewRepository.DestinationExists(desId)))
            {
                return NotFound();
            }

            var reviews4Destination = await _reviewRepository.GetReviewsForDestination(desId);
            var reviews4DestinationResults = _mapper.Map<IEnumerable<Review>>(reviews4Destination);

            return Ok(reviews4Destination);
        }

        //POST create a review 
        [HttpPost("{desId}/review")]
        public async Task<ActionResult<Review>> CreateReview(int desId, [FromBody] Review4CreationOrUpdateDto newReview)
        {
            if (newReview == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _reviewRepository.DestinationExists(desId)) return NotFound();

            var finalReview = _mapper.Map<Review>(newReview);

            await _reviewRepository.AddReviewForDestination(desId, finalReview);

            if (!await _reviewRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var createdReviewToReturn = _mapper.Map<ReviewDto>(finalReview);

            return CreatedAtAction("GetReviews", new { desId = desId, id = createdReviewToReturn.DestinationId }, createdReviewToReturn);
        }
        // PUT api/<controller>/5
        [HttpPut("{desId}/reviews/{id}")]
        public async Task<ActionResult> UpdateReview(int desId, int reviewId, [FromBody] Review4CreationOrUpdateDto newReview)
        {
            if (newReview == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _reviewRepository.DestinationExists(desId)) return NotFound();

            Review previousReview = await _reviewRepository.GetReviewById(reviewId);

            if (previousReview == null) return NotFound();

            _mapper.Map(newReview, previousReview);

            if (!await _reviewRepository.Save())
            {
                return StatusCode(500, "A problem happened while updating review your request.");
            }

            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("reviews/{id}")]
        public async Task<ActionResult> DeleteReview(int id)
        {
            await _reviewRepository.DeleteReview(id);
            return NoContent();
        }
    }
}
