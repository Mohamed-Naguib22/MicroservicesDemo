using AutoMapper;
using InventoryService.Domain.Entities.ProductEntities;
using InventoryService.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Features.ProductFeatures.Commands.CreateProduct
{
    public sealed class CreateProductMapper : Profile
    {
        public CreateProductMapper()
        {
            CreateMap<ProductCreatedEvent, Product>();
        }
    }
}
