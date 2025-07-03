using AutoMapper;
using ProductService.Domain.Events.ProductEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Features.ProductFeatures.Commands.DeleteProduct
{
    public sealed class DeleteProductMapper : Profile
    {
        public DeleteProductMapper()
        {
            CreateMap<DeleteProductRequest, ProductDeletedEvent>();
        }
    }
}
