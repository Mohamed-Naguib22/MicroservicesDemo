using InventoryService.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Application.Contract.IRepositories.ICommon
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IReadOnlyList<T>> FindAsync(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>>? orderBy = null,
            bool ascending = true,
            int? limit = null
        );
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task UpdateManyAsync(List<T> entities);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    }
}
