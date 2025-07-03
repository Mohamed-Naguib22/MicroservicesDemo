using AutoMapper;
using InventoryService.Application.Contract.IInfrastructure.ICaching;
using InventoryService.Application.Contract.IInfrastructure.IRepositories.ICommon;
using InventoryService.Application.Exceptions;
using InventoryService.Application.Features.ProductFeatures.Commands.CreateProduct;
using InventoryService.Application.Features.ProductFeatures.Queries.GetProductById;
using InventoryService.Application.Mediator.Common;
using InventoryService.Domain.Constants;
using InventoryService.Domain.Entities.ProductEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Features.ProductFeatures.Commands.DeleteProduct
{
    public sealed class DeleteProductHandler(IUnitOfWork unitOfWork, IMapper mapper, ICachingService cachingService) : BaseHandler<Product, DeleteProductRequest, Unit>(unitOfWork, mapper)
    {
        private readonly ICachingService _cachingService = cachingService;

        public override async Task<Unit> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.ProductId) ?? throw new EntityNotFoundException();

            await _repository.DeleteAsync(product);

            await _unitOfWork.SaveChangesAsync();

            await _cachingService.RemoveDataAsync(RedisKeys.PRODUCTS_KEY);

            return Unit.Value;
        }
    }
}
