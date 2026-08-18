using MapsterMapper;
using InventoryService.Application.Abstractions.Mediator.Common;
using InventoryService.Application.Contract.IInfrastructure.IRepositories.ICommon;
using InventoryService.Domain.Entities.ProductEntities;
using MediatR;

namespace InventoryService.Application.Features.ProductFeatures.Queries.GetAllProducts
{
    public sealed class GetAllProductsHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseMappedRepositoryHandler<Product, GetAllProductsRequest, IEnumerable<GetAllProductsResponse>>(unitOfWork, mapper)
    {
        public override async Task<IEnumerable<GetAllProductsResponse>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.GetRepository<Product>().GetAllAsync();

            return _mapper.Map<IEnumerable<GetAllProductsResponse>>(products);
        }
    }
}