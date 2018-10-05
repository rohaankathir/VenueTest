using System;
using Autofac;
using Core.Framework;
using WebAPI.Repository.Dal;
using Xunit;

namespace WebAPI.Tests.Moq
{
    /// <summary>
    /// Text class is to Moq test methods
    /// </summary>
    /// ****************************************************
    /// Please following the naming of Test methods as following
    /// [UnitOfWork_StateUnderTest_ExpectedBehavior]
    /// e.g. MethodName _ TestParameter/ Condition _ Expected Result
    /// 
    /// Always Assert the results, Do not write anything to the console
    /// ****************************************************
    [Trait("CategoryName", "Moq")]
    public class CityDalTest: IClassFixture<BaseTestMoqFixture>, IDisposable
    {
        #region Declarations

        private ICityDal _iCityDal;
        private readonly IContainer _iContainer;

        #endregion

        #region Constructor

        public CityDalTest(BaseTestMoqFixture fixture)
        {
            _iContainer = fixture.Container;
        }

        #endregion

        #region Unit Tests

        /// <summary>
        /// Check the database select of GetCities Method
        /// </summary>
        [Fact]
        public void GetCities_GetAll_ShouldReturnListOfCities()
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iCityDal = scope.Resolve<ICityDal>();

                var cityList = _iCityDal.GetCities();

                Assert.True(cityList.Count > 0);
            }
        }

        /// <summary>
        /// Test the database select of GetCityById method
        /// </summary>
        /// <param name="cityId">int CityId</param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetCityById_ExistingId_ShouldReturnValue(int cityId)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iCityDal = scope.Resolve<ICityDal>();

                var cityList = _iCityDal.GetCityById(cityId);

                Assert.True(cityList != null);
            }
        }

        /// <summary>
        /// Test the database select of GetCityById where there is no results returned
        /// </summary>
        /// <param name="cityId">int CityId</param>
        [Theory]
        [InlineData(1000000)]
        public void GetCityById_NonExistingValue_ShouldNotReturnValue(int cityId)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iCityDal = scope.Resolve<ICityDal>();

                var cityList = _iCityDal.GetCityById(cityId);

                Assert.True(cityList == null);
            }
        }

        /// <summary>
        /// Test database integration of SaveCity method
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="Name"></param>
        [Theory]
        [InlineData(94, "Colobmo")]
        [InlineData(94, "Kandy")]
        public void SaveCity_SaveNewCity_ShouldSaveSuccessfully(int countryId, string Name)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iCityDal = scope.Resolve<ICityDal>();

                var city = new City { CountryId = countryId, Name = Name };

                var cityList = _iCityDal.SaveCity(city);

                Assert.True(cityList != null);
            }
        }

        /// <summary>
        /// Test database integration of SaveCity method with Delete command
        /// </summary>
        /// <param name="Name"></param>
        [Theory]
        [InlineData("Colobmo")]
        [InlineData("Kandy")]
        public void SaveCity_DeleteExistingCity_ShouldDeleteSuccessfully(string Name)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iCityDal = scope.Resolve<ICityDal>();

                var cityResult = _iCityDal.GetCityByName(Name);

                cityResult = _iCityDal.SaveCity(cityResult, true);

                Assert.True(cityResult == null);
            }
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
