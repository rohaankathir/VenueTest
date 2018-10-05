using System.Linq;

namespace WebAPI.Repository.Common
{
    public interface IGenericRepository<T> where T: class
    {
        IQueryable<T> GetAll();
    }
}