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
    public class DestinationController : ControllerBase
    {
        private IDestinationRepository _destinationRepository;
        private readonly IMapper _mapper;
        DestinationController(IDestinationRepository destinationRepository, IMapper mapper)
        {
            _destinationRepository = destinationRepository;
            _mapper = mapper;
        }
        // GET: api/<controller>
        [HttpGet]
        [Route("/api/destinations")]
        public async Task<ActionResult<Destination>> GetListDestinations()
        {
            var destinations = await _destinationRepository.GetListDestinations();
            var results = _mapper.Map<IEnumerable<DestinationWithoutReviewsDto>>(destinations);
            return Ok(results);
        }
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Destination>> GetDestinationById(int id, bool includeReview = false)
        {
            var destination = await _destinationRepository.GetDestinationById(id, includeReview);
            if (destination == null) { return NotFound(); }
            if (includeReview)
            {
                var result = _mapper.Map<DestinationDto>(destination);
                return Ok(result);
            }
            var destinationWithoutReviewsResult = _mapper.Map<DestinationWithoutReviewsDto>(destination);
            return Ok(destinationWithoutReviewsResult);
        }
        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody] string value) { }
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAReviewOfRestaurant(int desId, int reviewId, [FromBody] Review review) {
            if (review == null || !ModelState.IsValid) return BadRequest();
            if (!await _destinationRepository.DestinationExists(desId)) return NotFound();
            IEnumerable<Review> previousReviews = await _destinationRepository.GetReviewsForDestination(desId);
            if (previousReviews == null) return NotFound();
            _mapper.Map(review, previousReviews);
            if(!await _destinationRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            return NoContent();
        }
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDestination(int id) {
            await _destinationRepository.DeleteDestination(id);
            return Ok();
        }
    }
}