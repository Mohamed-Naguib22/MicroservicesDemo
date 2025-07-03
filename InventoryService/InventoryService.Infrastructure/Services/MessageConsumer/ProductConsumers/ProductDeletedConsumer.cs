using InventoryService.Application.Features.ProductFeatures.Commands.CreateProduct;
using InventoryService.Application.Features.ProductFeatures.Commands.DeleteProduct;
using InventoryService.Domain.Events;
using InventoryService.Infrastructure.Services.MessageConsumer.Common;
using InventoryService.Infrastructure.Settings;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Infrastructure.Services.MessageConsumer.ProductConsumers
{
    public sealed class ProductDeletedConsumer(IOptions<RabbitMQSettings> options, ILogger<ProductDeletedConsumer> logger, IMediator mediator) : BaseRabbitMQConsumer<ProductDeletedEvent>(options, logger)
    {
        private readonly IMediator _mediator = mediator;

        protected override async Task HandleMessageAsync(ProductDeletedEvent @event)
        {
            await _mediator.Send(new DeleteProductRequest(@event.ProductId));
        }
    }
}
