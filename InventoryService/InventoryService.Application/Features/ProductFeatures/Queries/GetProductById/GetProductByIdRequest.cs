using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Features.ProductFeatures.Queries.GetProductById
{
    public sealed record GetProductByIdRequest(string Id) : IRequest<GetProductByIdResponse>;
}
