using WebAPI.Repository.Dal;

namespace WebAPI.Repository.Common
{
    public interface IDbFactory
    {
        VenueEntities Init();
    }
}