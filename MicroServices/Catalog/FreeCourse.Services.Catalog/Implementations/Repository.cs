using FreeCourse.Services.Catalog.Interfaces;
using FreeCourse.Services.Catalog.Settings;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace FreeCourse.Services.Catalog.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity>
       where TEntity : class
    {
        private readonly IMongoCollection<TEntity> _categoryCollection;

        public Repository(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(databaseSettings.DatabaseName);
            //_categoryCollection = database.GetCollection<TEntity>(databaseSettings.CourseCollectionName);
        }
        public async Task CreateAsync(TEntity entity)
        {
           throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> exp = null, params string[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> exp = null, params string[] includes)
        {
            throw new NotImplementedException();
        }

        public void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
