using Microsoft.EntityFrameworkCore;
using ProductService.Application.Contract.IRepositories.ICommon;
using ProductService.Application.Contract.IRepositories.IEventStoreRepositories;
using ProductService.Persistence.Context;
using ProductService.Persistence.Repositories.EventStoreRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Persistence.Repositories.Common
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly EventStoreDbContext _context;
        public IEventStoreRepository EventStoreRepository { get; private set; }

        public UnitOfWork(EventStoreDbContext context)
        {
            _context = context;
            EventStoreRepository = new EventStoreRepository(_context);
        }

        public IBaseRepository<T> GetRepository<T>() where T : class
        {
            return new BaseRepository<T>(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
