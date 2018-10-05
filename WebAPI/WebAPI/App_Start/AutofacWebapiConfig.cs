using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.Owin.Builder;
using WebAPI.Repository.Dal;

namespace WebAPI
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            // Register your MVC controllers.
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);

            //Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<VenueEntities>().As<DbContext>().InstancePerRequest();

            builder.RegisterType<CityDal>().As<ICityDal>().InstancePerRequest();
            builder.RegisterType<CountryDal>().As<ICountryDal>().InstancePerRequest();
            builder.RegisterType<CurrencyDal>().As<ICurrencyDal>().InstancePerRequest();
            builder.RegisterType<EventStyleDal>().As<IEventStyleDal>().InstancePerRequest();
            builder.RegisterType<EventTypeDal>().As<IEventTypeDal>().InstancePerRequest();
            builder.RegisterType<FacilityDal>().As<IFacilityDal>().InstancePerRequest();
            builder.RegisterType<MemberTypeDal>().As<IMemberTypeDal>().InstancePerRequest();
            builder.RegisterType<UserDal>().As<IUserDal>().InstancePerRequest();
            builder.RegisterType<UserGroupDal>().As<IUserGroupDal>().InstancePerRequest();
            builder.RegisterType<VenueDal>().As<IVenueDal>().InstancePerRequest();
            builder.RegisterType<VenueTypeDal>().As<IVenueTypeDal>().InstancePerRequest();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }
    }
}