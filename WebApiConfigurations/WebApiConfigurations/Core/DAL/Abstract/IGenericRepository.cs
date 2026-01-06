using System.Linq.Expressions;
using WebApiConfigurations.Entities;

namespace WebApiAdvance.Core.DAL.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity : class, new()
    {
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> func = null, params string[] includes);
        Task<List<TEntity>> GetAllPaginatedAsync(int page, int size, Expression<Func<TEntity, bool>> func = null, params string[] includes);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> func, params string[] includes);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(Guid id);
        Task SaveAsync();
    }
}
