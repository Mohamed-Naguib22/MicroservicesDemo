using AutoMapper;
using InventoryService.Application.Contract.IInfrastructure.IRepositories.ICommon;
using InventoryService.Application.Mediator.Common;
using InventoryService.Domain.Constants;
using InventoryService.Domain.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Features.ProductFeatures.Queries.GetProductById
{
    public sealed class GetProductByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<Product, GetProductByIdRequest, GetProductByIdResponse>(unitOfWork, mapper)
    {
        public override async Task<GetProductByIdResponse> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.GetRepository<Product>().FirstOrDefaultAsync(new GetProductByIdSpecification(request.Id));

            return _mapper.Map<GetProductByIdResponse>(products);
        }
    }
}
