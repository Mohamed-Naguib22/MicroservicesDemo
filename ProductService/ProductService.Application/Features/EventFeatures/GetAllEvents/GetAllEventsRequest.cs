using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Features.EventFeatures.GetAllEvents
{
    public sealed record GetAllEventsRequest : IRequest<IEnumerable<GetAllEventsResponse>>;
}
