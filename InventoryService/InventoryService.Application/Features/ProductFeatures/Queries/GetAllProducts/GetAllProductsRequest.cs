using InventoryService.Application.Contract.IInfrastructure.ICaching;
using InventoryService.Domain.Constants;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Features.ProductFeatures.Queries.GetAllProducts
{
    public sealed record GetAllProductsRequest : IRequest<IEnumerable<GetAllProductsResponse>>, ICacheableRequest
    {
        public string CacheKey => RedisKeys.PRODUCTS_KEY;

        public TimeSpan Expiry => TimeSpan.FromHours(6);
    }
}
