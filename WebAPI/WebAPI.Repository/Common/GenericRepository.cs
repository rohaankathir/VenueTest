using System.Linq;
using WebAPI.Repository.Dal;

namespace WebAPI.Repository.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T: class
    {
        private VenueEntities dbContext;

        protected IDbFactory DbFactory { get; private set; }

        protected VenueEntities DbContext => dbContext ?? (dbContext = DbFactory.Init());

        public GenericRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
        }

        public IQueryable<T> GetAll()
        {
            return DbContext.Set<T>();
        }
    }
}