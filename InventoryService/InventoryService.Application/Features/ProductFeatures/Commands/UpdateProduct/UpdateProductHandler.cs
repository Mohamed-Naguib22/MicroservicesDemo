using AutoMapper;
using InventoryService.Application.Abstractions.Mediator.Common;
using InventoryService.Application.Contract.IInfrastructure.IRepositories.ICommon;
using InventoryService.Application.Exceptions;
using InventoryService.Domain.Entities.ProductEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Features.ProductFeatures.Commands.UpdateProduct
{
    public sealed class UpdateProductHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<Product, UpdateProductRequest, Unit>(unitOfWork, mapper)
    {
        public override async Task<Unit> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.ProductId) ?? throw new EntityNotFoundException();

            _mapper.Map(request.UpdatedProduct, product);

            await _repository.UpdateAsync(product);

            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
