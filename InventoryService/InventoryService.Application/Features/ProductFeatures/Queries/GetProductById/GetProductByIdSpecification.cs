using InventoryService.Application.Specifications.Common;
using InventoryService.Domain.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Features.ProductFeatures.Queries.GetProductById
{
    public sealed class GetProductByIdSpecification : BaseSpecification<Product>
    {
        public GetProductByIdSpecification(string id)
        {
            AddCriteria(product => product.Id == id);
        }
    }
}
