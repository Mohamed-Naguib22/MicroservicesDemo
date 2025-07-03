using InventoryService.Application.Contract.ISpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Specifications.Common
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; protected set; } = x => true;
        public Expression<Func<T, object>>? OrderBy { get; protected set; }
        public bool IsAscending { get; protected set; } = true;
        public int? Take { get; protected set; }
        public void AddCriteria(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public void ApplyOrderBy(Expression<Func<T, object>> orderBy, bool ascending = true)
        {
            OrderBy = orderBy;
            IsAscending = ascending;
        }
        public void ApplyPaging(int take)
        {
            Take = take;
        }
    }
}
