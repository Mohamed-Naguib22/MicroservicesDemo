using AutoMapper;
using InventoryService.Domain.Entities.ProductEntities;
using InventoryService.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Features.ProductFeatures.Commands.UpdateProduct
{
    public sealed class UpdateProductMapper : Profile
    {
        public UpdateProductMapper() 
        {
            CreateMap<UpdatedProduct, Product>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
