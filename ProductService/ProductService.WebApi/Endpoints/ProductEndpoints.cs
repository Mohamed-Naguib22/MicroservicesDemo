using MediatR;
using ProductService.Application.Features.ProductFeatures.Commands.CreateProduct;
using ProductService.Application.Features.ProductFeatures.Commands.DeleteProduct;
using ProductService.Application.Features.ProductFeatures.Commands.UpdateProduct;

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

            producttGroup.MapPut($"Update/{{productId}}", async (IMediator mediator, string productId, UpdateProductDto updateProductDto) =>
            {
                var response = await mediator.Send(new UpdateProductRequest(productId, updateProductDto));

                return Results.Ok();
            });

            producttGroup.MapDelete($"Delete/{{productId}}", async (IMediator mediator, string productId) =>
            {
                var response = await mediator.Send(new DeleteProductRequest(productId));

                return Results.Ok();
            });
        }
    }
}
