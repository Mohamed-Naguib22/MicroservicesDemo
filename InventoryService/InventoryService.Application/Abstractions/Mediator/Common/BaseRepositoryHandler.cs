using InventoryService.Application.Contract.IInfrastructure.IRepositories.ICommon;
using InventoryService.Domain.Entities.Common;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Application.Abstractions.Mediator.Common
{
    public abstract class BaseRepositoryHandler<TEntity, TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TEntity : BaseEntity
    where TRequest : IRequest<TResponse>
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IBaseRepository<TEntity> _repository;

        protected BaseRepositoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TEntity>();
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
