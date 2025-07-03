using InventoryService.Application.Features.ProductFeatures.Commands.CreateProduct;
using InventoryService.Application.Features.ProductFeatures.Commands.UpdateProduct;
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
    public sealed class ProductUpdatedConsumer(IOptions<RabbitMQSettings> options, ILogger<ProductUpdatedConsumer> logger, IMediator mediator) : BaseRabbitMQConsumer<ProductUpdatedEvent>(options, logger)
    {
        private readonly IMediator _mediator = mediator;

        protected override async Task HandleMessageAsync(ProductUpdatedEvent @event)
        {
            await _mediator.Send(new UpdateProductRequest(@event.ProductId, @event.UpdatedProduct));
        }
    }
}
