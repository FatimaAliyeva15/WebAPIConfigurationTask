using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiAdvance.DAL.Repositories.AbstractRepositories;
using WebApiConfigurations.DAL.EFCore;
using WebApiConfigurations.Entities;

namespace WebApiAdvance.DAL.Repositories.ConcretesRepositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _appDbContext.Categories.AddAsync(category);
            
        }

        public void DeleteCategory(Guid id)
        {
            var exsistCategory = _appDbContext.Categories.Find(id);
            _appDbContext.Categories.Remove(exsistCategory);
        }

        public Task<List<Category>> GetAllCategoryAsync(Expression<Func<Category, bool>> func = null, params string[] includes)
        {
            IQueryable<Category> query = GetQuery(includes);

            return func == null 
                ? query.ToListAsync() 
                : query.Where(func).ToListAsync();
        }

        public Task<List<Category>> GetAllPaginatedAsync(int page, int size, Expression<Func<Category, bool>> func = null, params string[] includes)
        {
            IQueryable<Category> query = GetQuery(includes);
            return func == null 
                ? query.Skip((page - 1) * size).Take(size).ToListAsync() 
                : query.Where(func).Skip((page - 1) * size).Take(size).ToListAsync();

        }

        public Task<Category> GetCategory(Expression<Func<Category, bool>> func, params string[] includes)
        {
            IQueryable<Category> query = GetQuery(includes);

            return query.Where(func).FirstOrDefaultAsync();
        }

        public async Task SaveAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public void UpdateCategory(Category category)
        {
            _appDbContext.Categories.Update(category);
        }

        IQueryable<Category> GetQuery(string[] includes)
        {
            IQueryable<Category> query = _appDbContext.Categories;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }
    }
}
