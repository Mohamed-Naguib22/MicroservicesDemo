using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Features.ProductFeatures.Queries.GetAllProducts
{
    public sealed record GetAllProductsResponse(string Id, string Name, int Quantity);
}
