using AutoMapper;
using InventoryService.Domain.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Features.ProductFeatures.Queries.GetAllProducts
{
    public sealed class GetAllProductsMapper : Profile
    {
        public GetAllProductsMapper()
        {
            CreateMap<Product, GetAllProductsResponse>();
        }
    }
}
