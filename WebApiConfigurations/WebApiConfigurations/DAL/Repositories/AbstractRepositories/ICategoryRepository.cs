using System.Linq.Expressions;
using WebApiConfigurations.Entities;

namespace WebApiAdvance.DAL.Repositories.AbstractRepositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoryAsync(Expression<Func<Category, bool>> func = null, params string[] includes);
        Task<List<Category>> GetAllPaginatedAsync(int page, int size, Expression<Func<Category, bool>> func = null, params string[] includes);
        Task<Category> GetCategory(Expression<Func<Category, bool>> func, params string[] includes);
        Task AddCategoryAsync(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Guid id);
        Task SaveAsync();
    }
}
