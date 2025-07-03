    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    namespace InventoryService.WebApi.Controllers.Common
    {
        [ApiController]
        [Route("api/[controller]/[Action]")]
        public class BaseMediatorController(IMediator mediator) : ControllerBase
        {
            protected readonly IMediator _mediator = mediator;
            //protected async Task<IActionResult> SendAsync<TRequest>(TRequest request) where TRequest : IRequest<object>
            //{
            //    return Ok(await _mediator.Send(request));
            //}
        }
    }
