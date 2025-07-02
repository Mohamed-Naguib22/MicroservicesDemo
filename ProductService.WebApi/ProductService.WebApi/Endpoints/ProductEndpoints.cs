using MediatR;
using ProductService.Application.Features.ProductFeatures.Commands.CreateProduct;

namespace ProductService.WebApi.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            var producttGroup = app.MapGroup("api/Product/");

            producttGroup.MapPost($"Create", async (IMediator mediator, CreateProductRequest request) =>
            {
                var response = await mediator.Send(request);

                return Results.Ok();
            });

        }
    }
}
