using WebAPI.Repository.Dal;

namespace WebAPI.Repository.Common
{
    public class DbFactory: IDbFactory
    {
        private VenueEntities dbContext;

        public VenueEntities Init()
        {
            return dbContext ?? (dbContext = new VenueEntities());
        }
    }
}