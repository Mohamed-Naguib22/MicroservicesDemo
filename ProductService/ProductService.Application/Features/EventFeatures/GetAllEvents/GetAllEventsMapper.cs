using AutoMapper;
using ProductService.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Features.EventFeatures.GetAllEvents
{
    public sealed class GetAllEventsMapper : Profile
    {
        public GetAllEventsMapper()
        {
            CreateMap<EventEntity, GetAllEventsResponse>();
        }
    }
}
