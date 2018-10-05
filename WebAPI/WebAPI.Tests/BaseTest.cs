using System.Data.Entity;
using Autofac;
using WebAPI.Repository.Dal;

namespace WebAPI.Tests
{
    /// <summary>
    /// Base Test Fixture class
    /// </summary>
    public class BaseTestFixture
    {
        #region Declarations

        public IContainer Container { get; set; }

        private ContainerBuilder Builder { get; }

        #endregion

        #region Constructor
        
        public BaseTestFixture()
        {
            Builder = new ContainerBuilder();

            RegisterServices(Builder);
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Register Services to Container
        /// </summary>
        /// <param name="builder"></param>
        public void RegisterServices(ContainerBuilder builder)
        {
            // Register Database Entity
            builder.RegisterType<VenueEntities>().As<DbContext>().InstancePerRequest();

            // Register Interfaces
            builder.RegisterType<CityDal>().As<ICityDal>().InstancePerDependency();
            builder.RegisterType<CountryDal>().As<ICountryDal>().InstancePerDependency();
            builder.RegisterType<CurrencyDal>().As<ICurrencyDal>().InstancePerDependency();
            builder.RegisterType<EventStyleDal>().As<IEventStyleDal>().InstancePerDependency();
            builder.RegisterType<EventTypeDal>().As<IEventTypeDal>().InstancePerDependency();
            builder.RegisterType<FacilityDal>().As<IFacilityDal>().InstancePerDependency();
            builder.RegisterType<MemberTypeDal>().As<IMemberTypeDal>().InstancePerDependency();
            builder.RegisterType<UserDal>().As<IUserDal>().InstancePerDependency();
            builder.RegisterType<UserGroupDal>().As<IUserGroupDal>().InstancePerDependency();
            builder.RegisterType<VenueDal>().As<IVenueDal>().InstancePerDependency();
            builder.RegisterType<VenueTypeDal>().As<IVenueTypeDal>().InstancePerDependency();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();
        }

        #endregion
    }
}
