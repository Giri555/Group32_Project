using AutoMapper;
using Group32_API.DTOs;
using Group32_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group32_API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DestinationInfo, DestinationWithoutReviewsDto>();
            CreateMap<DestinationInfo, DestinationDto>();
            CreateMap<DestinationInfo, Destination4CreationOrUpdateDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<Review, Review4CreationOrUpdateDto>();
        }
    }
}
