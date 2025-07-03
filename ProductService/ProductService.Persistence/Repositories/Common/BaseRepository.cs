using Microsoft.EntityFrameworkCore;
using ProductService.Application.Contract.IInfrastructure.IRepositories.ICommon;
using ProductService.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Persistence.Repositories.Common
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly EventStoreDbContext _context;
        private readonly DbSet<T> Entities;

        public BaseRepository(EventStoreDbContext context)
        {
            _context = context;
            Entities = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = Entities.AsQueryable();

            return await query.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await Entities.AddAsync(entity);
        }

        public async Task AddRangeAsync(List<T> entity)
        {
            await Entities.AddRangeAsync(entity);
        }
    }
}
