using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiAdvance.Core.DAL.Concrates;
using WebApiAdvance.DAL.Repositories.AbstractRepositories;
using WebApiConfigurations.DAL.EFCore;
using WebApiConfigurations.Entities;

namespace WebApiAdvance.DAL.Repositories.ConcretesRepositories
{
    public class CategoryRepository : GenericRepository<Category, AppDbContext>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
