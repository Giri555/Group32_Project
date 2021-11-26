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
        public DestinationController(IDestinationRepository destinationRepository, IMapper mapper)
        {
            _destinationRepository = destinationRepository;
            _mapper = mapper;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<DestinationInfo>> GetListDestinations()
        {
            var destinations = await _destinationRepository.GetListDestinations();
            var results = _mapper.Map<IEnumerable<DestinationWithoutReviewsDto>>(destinations);
            return Ok(results);
        }
        [HttpGet("restaurant")]
        public async Task<ActionResult<DestinationInfo>> GetListRestaurant()
        {
            var destinations = await _destinationRepository.GetAllRestaurant("Restaurant");
            var results = _mapper.Map<IEnumerable<DestinationWithoutReviewsDto>>(destinations);
            return Ok(results);
        }
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DestinationInfo>> GetDestinationById(int id, bool includeReview = false)
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
        public async Task<ActionResult<DestinationInfo>> CreateDestination([FromBody] Destination4CreationOrUpdateDto destination) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (destination == null) return BadRequest();
            var result = _mapper.Map<DestinationInfo>(destination);
            _destinationRepository.CreateDestination(result);
            if (!await _destinationRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            return NoContent();
        }
        // PUT api/<controller>/5
        [HttpPut("{desId}")]
        public async Task<ActionResult> UpdateDestination(int desId, [FromBody] Destination4CreationOrUpdateDto destination)
        {
            if (destination == null || !ModelState.IsValid) return BadRequest();
            if (!await _destinationRepository.DestinationExists(desId)) return NotFound();
            DestinationInfo previousDestination = await _destinationRepository.GetDestinationById(desId,false);
            if (previousDestination == null) return NotFound();
            _mapper.Map(destination, previousDestination);
            if (!await _destinationRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            return NoContent();
        }
        // PUT api/<controller>/5
        [HttpPut("{desId}/reviews")]
        public async Task<ActionResult> UpdateAReviewOfRestaurant(int desId, [FromBody] Review review)
        {
            if (review == null || !ModelState.IsValid) return BadRequest();
            if (!await _destinationRepository.DestinationExists(desId)) return NotFound();
            IEnumerable<Review> previousReviews = await _destinationRepository.GetReviewsForDestination(desId);
            if (previousReviews == null) return NotFound();
            _mapper.Map(review, previousReviews);
            if (!await _destinationRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            return NoContent();
        }
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDestination(int id) {
            await _destinationRepository.DeleteDestination(id);
            if (!await _destinationRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            return Ok();
        }
    }
}