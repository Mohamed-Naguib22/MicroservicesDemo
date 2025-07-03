using InventoryService.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Features.ProductFeatures.Commands.UpdateProduct
{
    public sealed record UpdateProductRequest(string ProductId, UpdatedProduct UpdatedProduct) : IRequest<Unit>;
}
