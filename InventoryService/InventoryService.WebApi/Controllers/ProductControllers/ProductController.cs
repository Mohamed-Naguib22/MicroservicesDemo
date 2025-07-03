using InventoryService.Application.Features.ProductFeatures.Queries.GetAllProducts;
using InventoryService.WebApi.Controllers.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.WebApi.Controllers.ProductControllers
{
    public class ProductController(IMediator mediator) : BaseMediatorController(mediator)
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllProductsRequest()));
        }
    }
}
