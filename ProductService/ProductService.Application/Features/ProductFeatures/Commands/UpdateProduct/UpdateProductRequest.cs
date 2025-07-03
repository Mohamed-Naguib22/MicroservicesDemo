using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Features.ProductFeatures.Commands.UpdateProduct
{
    public sealed record UpdateProductRequest(string ProductId, UpdateProductDto UpdateProductDto) : IRequest<Unit>;
    public sealed record UpdateProductDto(string? Name, int? Quantity);
}
