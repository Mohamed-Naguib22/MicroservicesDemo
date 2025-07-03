using AutoMapper;
using InventoryService.Domain.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Features.ProductFeatures.Queries.GetProductById
{
    public sealed class GetProductByIdMapper : Profile
    {
        public GetProductByIdMapper()
        {
            CreateMap<Product, GetProductByIdResponse>();
        }
    }
}
