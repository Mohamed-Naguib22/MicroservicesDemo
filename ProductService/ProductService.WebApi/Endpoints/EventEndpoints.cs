using MediatR;
using ProductService.Application.Features.EventFeatures.GetAllEvents;

namespace ProductService.WebApi.Endpoints
{
    public static class EventEndpoints
    {
        public static void MapEventsEndpoints(this IEndpointRouteBuilder app)
        {
            var producttGroup = app.MapGroup("api/Event/");

            producttGroup.MapGet($"GetAll", async (IMediator mediator) =>
            {
                return Results.Ok(await mediator.Send(new GetAllEventsRequest()));
            });
        }
    }
}
