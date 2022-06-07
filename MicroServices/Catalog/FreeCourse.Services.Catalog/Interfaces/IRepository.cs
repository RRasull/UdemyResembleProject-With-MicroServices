using System.Linq.Expressions;

namespace FreeCourse.Services.Catalog.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> exp = null, params string[] includes);
        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> exp = null, params string[] includes);

        Task CreateAsync(TEntity entity);

        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
