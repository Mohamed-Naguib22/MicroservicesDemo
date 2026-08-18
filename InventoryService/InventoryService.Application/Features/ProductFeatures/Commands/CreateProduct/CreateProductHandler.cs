using MapsterMapper;
using InventoryService.Application.Contract.IInfrastructure.IRepositories.ICommon;
using InventoryService.Domain.Entities.ProductEntities;
using MediatR;
using InventoryService.Application.Abstractions.Mediator.Common;

namespace InventoryService.Application.Features.ProductFeatures.Commands.CreateProduct
{
    public sealed class CreateProductHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseMappedRepositoryHandler<Product, CreateProductRequest, Unit>(unitOfWork, mapper)
    {
        public override async Task<Unit> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.ProductCreatedEvent);

            await _repository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}