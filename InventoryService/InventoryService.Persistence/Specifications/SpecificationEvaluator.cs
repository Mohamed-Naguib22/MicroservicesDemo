using InventoryService.Application.Contract.ISpecifications;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Persistence.Specifications
{
    public static class SpecificationEvaluator<T> where T : class
    {
        public static IFindFluent<T, T> GetQuery(IMongoCollection<T> collection, ISpecification<T> specification)
        {
            var query = collection.Find(specification.Criteria);

            if (specification.OrderBy != null)
            {
                query = specification.IsAscending
                    ? query.SortBy(specification.OrderBy)
                    : query.SortByDescending(specification.OrderBy);
            }

            if (specification.Take.HasValue)
            {
                query = query.Limit(specification.Take.Value);
            }

            return query;
        }
    }
}
