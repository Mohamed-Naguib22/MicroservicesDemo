using AutoMapper;
using MediatR;
using ProductService.Application.Contract.IInfrastructure.IEventDispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Mediator.Common
{
    public abstract class BaseEventHandler<TEvent, TRequest>(IEventDispatcher eventDispatcher, IMapper mapper) : IRequestHandler<TRequest, Unit> where TRequest : IRequest<Unit>
    {
        protected readonly IEventDispatcher _eventDispatcher = eventDispatcher;
        protected readonly IMapper _mapper = mapper;

        public async virtual Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var @event =_mapper.Map<TEvent>(request);

            await _eventDispatcher.AppendAndPublishEventAsync(@event);

            return Unit.Value;
        }
    }
}
