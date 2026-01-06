using System.Linq.Expressions;
using WebApiAdvance.Core.DAL.Abstract;
using WebApiConfigurations.Entities;

namespace WebApiAdvance.DAL.Repositories.AbstractRepositories
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
    }
}
