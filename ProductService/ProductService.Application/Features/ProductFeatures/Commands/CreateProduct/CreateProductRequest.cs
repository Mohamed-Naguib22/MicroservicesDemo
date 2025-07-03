using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Features.ProductFeatures.Commands.CreateProduct
{
    public sealed record CreateProductRequest(string Name, int Quantity) : IRequest<Unit>;
}
