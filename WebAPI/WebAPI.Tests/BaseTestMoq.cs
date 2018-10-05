using Autofac;
using Moq;
using WebAPI.Repository.Dal;

namespace WebAPI.Tests
{
    /// <summary>
    /// Base test class for Moq fixtures.
    /// We register interfaces and Entities here and mock them
    /// </summary>
    public class BaseTestMoqFixture
    {
        #region Declarations
        
        public IContainer Container { get; set; }

        private ContainerBuilder Builder { get; set; }

        #endregion

        #region Constructor
        
        public BaseTestMoqFixture()
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
            builder.RegisterInstance(new Mock<VenueEntities>().Object).As<VenueEntities>();

            // Register Interfaces
            builder.RegisterInstance(new Mock<ICityDal>().Object).As<ICityDal>();
            builder.RegisterInstance(new Mock<ICountryDal>().Object).As<ICountryDal>();
            builder.RegisterInstance(new Mock<ICurrencyDal>().Object).As<ICurrencyDal>();
            builder.RegisterInstance(new Mock<IEventStyleDal>().Object).As<IEventStyleDal>();
            builder.RegisterInstance(new Mock<IEventStyleDal>().Object).As<IEventStyleDal>();
            builder.RegisterInstance(new Mock<IEventTypeDal>().Object).As<IEventTypeDal>();
            builder.RegisterInstance(new Mock<IFacilityDal>().Object).As<IFacilityDal>();
            builder.RegisterInstance(new Mock<IMemberTypeDal>().Object).As<IMemberTypeDal>();
            builder.RegisterInstance(new Mock<IUserDal>().Object).As<IUserDal>();
            builder.RegisterInstance(new Mock<IUserGroupDal>().Object).As<IUserGroupDal>();
            builder.RegisterInstance(new Mock<IVenueDal>().Object).As<IVenueDal>();
            builder.RegisterInstance(new Mock<IVenueTypeDal>().Object).As<IVenueTypeDal>();
            

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();
        }

        #endregion
    }
}
