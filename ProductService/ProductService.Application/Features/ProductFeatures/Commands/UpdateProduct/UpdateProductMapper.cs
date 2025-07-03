using AutoMapper;
using ProductService.Domain.Events.ProductEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Features.ProductFeatures.Commands.UpdateProduct
{
    public sealed class UpdateProductMapper : Profile
    {
        public UpdateProductMapper()
        {
            CreateMap<UpdateProductRequest, ProductUpdatedEvent>()
                .ForMember(dest => dest.UpdatedProduct, opt => opt.MapFrom(src => src.UpdateProductDto));

            CreateMap<UpdateProductDto, UpdatedProduct>();
        }
    }
}
