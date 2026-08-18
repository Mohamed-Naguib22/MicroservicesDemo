using MapsterMapper;
using InventoryService.Application.Contract.IInfrastructure.IRepositories.ICommon;
using InventoryService.Domain.Entities.Common;
using MediatR;

namespace InventoryService.Application.Abstractions.Mediator.Common
{
    public abstract class BaseMappedRepositoryHandler<TEntity, TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TEntity : BaseEntity
        where TRequest : IRequest<TResponse>
    {
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IBaseRepository<TEntity> _repository;

        protected BaseMappedRepositoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TEntity>();
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}