using InventoryService.Application.Contract.IRepositories.ICommon;
using InventoryService.Domain.Constants;
using InventoryService.Domain.Entities.Common;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Persistence.Repositories
{
    public class UnitOfWork(IMongoDatabase database) : IUnitOfWork
    {
        private readonly IMongoDatabase _database = database;

        public IBaseRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return new BaseRepository<T>(_database, MongoCollectionNames.GetCollectionName<T>());
        }

        public async Task SaveChangesAsync()
        {
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
