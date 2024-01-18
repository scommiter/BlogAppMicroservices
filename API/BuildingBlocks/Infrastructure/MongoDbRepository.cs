using Contracts.Domains;
using Infrastructure.Extensions;
using Infrastructure.Interfaces;
using MongoDB.Driver;
using Shared.Configurations;
using System.Linq.Expressions;

namespace Infrastructure
{
    public class MongoDbRepository<T> : IMongoDbRepositoryBase<T> where T : MongoEntity
    {
        private IMongoDatabase Database { get; }

        public MongoDbRepository(IMongoClient client, DatabaseSettings settings)
        {
            Database = client.GetDatabase(settings.DatabaseName);
        }

        public Task CreateAsync(T entity) => Collection.InsertOneAsync(entity);

        public Task DeleteAsync(string id) => Collection.DeleteOneAsync(x => x.Id.Equals(id));

        public IMongoCollection<T> FindAll(ReadPreference? readPreference = null)
            => Database.WithReadPreference(readPreference ?? ReadPreference.Primary)
                .GetCollection<T>(GetCollectionName());

        protected virtual IMongoCollection<T> Collection =>
            Database.GetCollection<T>(GetCollectionName());

        public Task UpdateAsync(T entity)
        {
            Expression<Func<T, string>> func = f => f.Id;
            var value = (string)entity.GetType()
                .GetProperty(func.Body.ToString()
                .Split(".")[1])?.GetValue(entity, null);
            var filter = Builders<T>.Filter.Eq(func, value);

            return Collection.ReplaceOneAsync(filter, entity);
        }

        private static string GetCollectionName()
        {
            var collec = typeof(T).GetCustomAttributes(typeof(BsonCollectionAttribute), true)
                .FirstOrDefault() as BsonCollectionAttribute;
            return collec?.CollectionName;
        }
    }
}
