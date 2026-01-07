using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApiAdvance.Core.DAL.Abstract;
using WebApiConfigurations.DAL.EFCore;
using WebApiConfigurations.Entities;

namespace WebApiAdvance.Core.DAL.Concrates
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
        where TEntity : class, new()
        where TContext : DbContext
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<TEntity> _entities;

        public GenericRepository(AppDbContext context)
        {
            _appDbContext = context;
            _entities = _appDbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
        }

        public void Delete(Guid id)
        {
            var exsistEntity = _entities.Find(id);
            _entities.Remove(exsistEntity);
        }

        public Task<TEntity> Get(Expression<Func<TEntity, bool>> func, params string[] includes)
        {
            IQueryable<TEntity> query = GetQuery(includes);

            return query.Where(func).FirstOrDefaultAsync(); ;
        }

        public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> func = null, params string[] includes)
        {
            IQueryable<TEntity> query = GetQuery(includes);

            return func == null
                ? query.ToListAsync()
                : query.Where(func).ToListAsync();
        }

        public Task<List<TEntity>> GetAllPaginatedAsync(int page, int size, Expression<Func<TEntity, bool>> func = null, params string[] includes)
        {
            IQueryable<TEntity> query = GetQuery(includes);
            return func == null
                ? query.Skip((page - 1) * size).Take(size).ToListAsync()
                : query.Where(func).Skip((page - 1) * size).Take(size).ToListAsync();
        }

        

        public void Update(TEntity entity)
        {
            _entities.Update(entity);
        }

        IQueryable<TEntity> GetQuery(string[] includes)
        {
            IQueryable<TEntity> query = _entities;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }
    }
}
