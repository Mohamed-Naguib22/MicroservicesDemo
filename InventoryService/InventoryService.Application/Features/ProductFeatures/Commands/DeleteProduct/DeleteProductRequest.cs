using InventoryService.Application.Contract.IInfrastructure.ICaching;
using InventoryService.Domain.Constants;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Features.ProductFeatures.Commands.DeleteProduct
{
    public sealed record DeleteProductRequest(string ProductId) : IRequest<Unit>, ICacheInvalidatingRequest
    {
        public string CacheKeyToInvalidate => RedisKeys.PRODUCTS_KEY;
    }
}
