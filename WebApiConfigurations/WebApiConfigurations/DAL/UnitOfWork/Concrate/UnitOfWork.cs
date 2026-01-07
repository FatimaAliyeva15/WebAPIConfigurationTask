using WebApiAdvance.DAL.Repositories.AbstractRepositories;
using WebApiAdvance.DAL.Repositories.ConcretesRepositories;
using WebApiAdvance.DAL.UnitOfWork.Abstract;
using WebApiConfigurations.DAL.EFCore;

namespace WebApiAdvance.DAL.UnitOfWork.Concrate
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICategoryRepository _categoryRepository;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public ICategoryRepository CategoryRepository => _categoryRepository ?? new CategoryRepository(_appDbContext) ;


        public async Task SaveAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
