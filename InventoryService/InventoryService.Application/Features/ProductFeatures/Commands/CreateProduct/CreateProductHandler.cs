using AutoMapper;
using InventoryService.Application.Contract.IInfrastructure.ICaching;
using InventoryService.Application.Contract.IInfrastructure.IRepositories.ICommon;
using InventoryService.Application.Mediator.Common;
using InventoryService.Domain.Constants;
using InventoryService.Domain.Entities.ProductEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Features.ProductFeatures.Commands.CreateProduct
{
    public sealed class CreateProductHandler(IUnitOfWork unitOfWork, IMapper mapper, ICachingService cachingService) : BaseHandler<Product, CreateProductRequest, Unit>(unitOfWork, mapper)
    {
        private readonly ICachingService _cachingService = cachingService;

        public override async Task<Unit> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.ProductCreatedEvent);
            
            await _repository.AddAsync(product);

            await _unitOfWork.SaveChangesAsync();

            await _cachingService.RemoveDataAsync(RedisKeys.PRODUCTS_KEY);

            return Unit.Value;
        }
    }
}
