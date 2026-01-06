using WebApiAdvance.Core.DAL.Concrates;
using WebApiAdvance.DAL.Repositories.AbstractRepositories;
using WebApiConfigurations.DAL.EFCore;
using WebApiConfigurations.Entities;

namespace WebApiAdvance.DAL.Repositories.ConcreteRepositories
{
    public class ProductRepository : GenericRepository<Product, AppDbContext>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
