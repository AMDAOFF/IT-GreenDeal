using Energi.DataAccess.Entity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Energi.DataAccess.MongoDB
{
    public class MongoContext<T> : IContext<T> where T : IEntity
    {
        private readonly IMongoCollection<T> _collection;
        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;
        private readonly IMongoDatabase _database;
        private readonly string _collectionName;

        public MongoContext(IMongoDatabase database, string collectionName)
        {
            _database = database;            
            _collection = database.GetCollection<T>(collectionName);
            _collectionName = collectionName;
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await _collection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            FilterDefinition<T> filter = filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task RemoveAsync(int id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, id);
            await _collection.DeleteOneAsync(filter);
        }

        public async Task DropDatabase()
        {
            await _database.DropCollectionAsync(_collectionName);
        }
    }

}
