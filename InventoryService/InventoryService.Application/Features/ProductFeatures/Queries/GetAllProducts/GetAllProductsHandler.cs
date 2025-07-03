using AutoMapper;
using InventoryService.Application.Contract.IRepositories.ICommon;
using InventoryService.Application.Mediator.Common;
using InventoryService.Domain.Entities.ProductEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Features.ProductFeatures.Queries.GetAllProducts
{
    public sealed class GetAllProductsHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<Product, GetAllProductsRequest, IEnumerable<GetAllProductsResponse>>(unitOfWork, mapper)
    {
        public override async Task<IEnumerable<GetAllProductsResponse>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<GetAllProductsResponse>>(products);
        }
    }
}
