using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.WebApi.Controllers.Common
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class BaseMediatorController(IMediator mediator) : ControllerBase
    {
        protected readonly IMediator _mediator = mediator;
    }
}
