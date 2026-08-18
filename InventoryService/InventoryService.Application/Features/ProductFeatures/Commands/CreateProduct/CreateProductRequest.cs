using InventoryService.Application.Contract.IInfrastructure.ICaching;
using InventoryService.Domain.Constants;
using InventoryService.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Features.ProductFeatures.Commands.CreateProduct
{
    public sealed record CreateProductRequest(ProductCreatedEvent ProductCreatedEvent) : IRequest<Unit>, ICacheInvalidatingRequest
    {
        public string CacheKeyToInvalidate => RedisKeys.PRODUCTS_KEY;
    }
}
