using MapsterMapper;
using InventoryService.Application.Abstractions.Mediator.Common;
using InventoryService.Application.Contract.IInfrastructure.IRepositories.ICommon;
using InventoryService.Application.Exceptions;
using InventoryService.Domain.Entities.ProductEntities;

namespace InventoryService.Application.Features.ProductFeatures.Queries.GetProductById
{
    public sealed class GetProductByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseMappedRepositoryHandler<Product, GetProductByIdRequest, GetProductByIdResponse>(unitOfWork, mapper)
    {
        public override async Task<GetProductByIdResponse> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.GetRepository<Product>().FirstOrDefaultAsync(
                new GetProductByIdSpecification(request.Id)
            ) ?? throw new EntityNotFoundException();

            return _mapper.Map<GetProductByIdResponse>(product);
        }
    }
}