using InventoryService.Application.Contract.IInfrastructure.IRepositories.ICommon;
using InventoryService.Application.Contract.ISpecifications;
using InventoryService.Domain.Entities.Common;
using InventoryService.Persistence.Specifications;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Persistence.Repositories
{
    public class BaseRepository<T>(IMongoDatabase database, string collectionName) : IBaseRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _collection = database.GetCollection<T>(collectionName);

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            var result = await _collection.Find(_ => true).ToListAsync();
            return result.AsReadOnly();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _collection.Find(entity => entity.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await _collection.InsertManyAsync(entities);
        }

        public async Task UpdateAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task UpdateManyAsync(List<T> entities)
        {
            var bulkOperations = entities.Select(entity =>
            {
                var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
                return new ReplaceOneModel<T>(filter, entity);
            }).ToList();

            if (bulkOperations.Count != 0)
            {
                await _collection.BulkWriteAsync(bulkOperations);
            }
        }

        public async Task DeleteAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);

            await _collection.DeleteOneAsync(filter);
        }

        public async Task<IReadOnlyList<T>> FindAsync(ISpecification<T> spec) => (await SpecificationEvaluator<T>.GetQuery(_collection, spec).ToListAsync()).AsReadOnly();

        public async Task<T> FirstOrDefaultAsync(ISpecification<T> spec) => await SpecificationEvaluator<T>.GetQuery(_collection, spec).FirstOrDefaultAsync();

        public async Task<bool> ExistsAsync(ISpecification<T> spec) => await SpecificationEvaluator<T>.GetQuery(_collection, spec).AnyAsync();

        public async Task<int> CountAsync(ISpecification<T> spec) => (int)await _collection.CountDocumentsAsync(spec.Criteria);
    }
}
