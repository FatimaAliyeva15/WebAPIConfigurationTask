using WebApiAdvance.DAL.Repositories.AbstractRepositories;

namespace WebApiAdvance.DAL.UnitOfWork.Abstract
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        Task SaveAsync();
    }
}
