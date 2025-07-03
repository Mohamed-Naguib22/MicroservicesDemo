using AutoMapper;
using InventoryService.Application.Contract.IInfrastructure.ICaching;
using InventoryService.Application.Contract.IInfrastructure.IRepositories.ICommon;
using InventoryService.Application.Mediator.Common;
using InventoryService.Domain.Constants;
using InventoryService.Domain.Entities.ProductEntities;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Features.ProductFeatures.Queries.GetAllProducts
{
    public sealed class GetAllProductsHandler(IUnitOfWork unitOfWork, IMapper mapper, ICachingService cachingService) : BaseHandler<Product, GetAllProductsRequest, IEnumerable<GetAllProductsResponse>>(unitOfWork, mapper)
    {
        private readonly ICachingService _cachingService = cachingService;
        public override async Task<IEnumerable<GetAllProductsResponse>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            var products = Enumerable.Empty<Product>();
            var cahcedProducts = await _cachingService.GetDataAsync<IEnumerable<Product>>(RedisKeys.PRODUCTS_KEY);

            if (cahcedProducts is not null)
            {
                products = cahcedProducts;
            }
            else
            {
                products = await _unitOfWork.GetRepository<Product>().GetAllAsync();

                await _cachingService.SetDataAsync(RedisKeys.PRODUCTS_KEY, products);
            }

            return _mapper.Map<IEnumerable<GetAllProductsResponse>>(products);
        }
    }
}
