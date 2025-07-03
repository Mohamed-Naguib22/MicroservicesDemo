using AutoMapper;
using MediatR;
using ProductService.Application.Contract.IInfrastructure.IRepositories.ICommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Features.EventFeatures.GetAllEvents
{
    public sealed class GetAllEventsHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllEventsRequest, IEnumerable<GetAllEventsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<GetAllEventsResponse>> Handle(GetAllEventsRequest request, CancellationToken cancellationToken)
        {
            var events = await _unitOfWork.EventStoreRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<GetAllEventsResponse>>(events);
        }
    }
}
