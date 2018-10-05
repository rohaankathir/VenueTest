using System;
using Autofac;
using Core.Framework;
using WebAPI.Repository.Dal;
using Xunit;

namespace WebAPI.Tests.DalTests
{
    /// <summary>
    /// Text class to test Dal method with database calls
    /// </summary>
    /// ****************************************************
    /// Please following the naming of Test methods as following
    /// [UnitOfWork_StateUnderTest_ExpectedBehavior]
    /// e.g. MethodName _ TestParameter/ Condition _ Expected Result
    /// 
    /// Always Assert the results, Do not write anything to the console
    /// ****************************************************
    [Trait("CategoryName", "Unit")]
    public class CountryDalTest: IClassFixture<BaseTestFixture>, IDisposable
    {
        #region Declarations

        private ICountryDal _iCountryDal;
        private readonly IContainer _iContainer;

        #endregion

        #region Constructor

        public CountryDalTest(BaseTestFixture fixture)
        {
            _iContainer = fixture.Container;
        }

        #endregion

        #region Unit Tests

        /// <summary>
        /// Test database for GetCountries method
        /// </summary>
        [Fact]
        public void GetCountries_GetAll_ShouldReturnListOfCounties()
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iCountryDal = scope.Resolve<ICountryDal>();

                var countryList = _iCountryDal.GetCountries();

                Assert.True(countryList.Count > 0);
            }
        }

        /// <summary>
        /// Test database for GetCountryById method.
        /// </summary>
        /// <param name="countryId"></param>
        [Theory]
        [InlineData(61)]
        [InlineData(64)]
        public void GetCountryById_ExistingId_ShouldReturnValue(int countryId)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iCountryDal = scope.Resolve<ICountryDal>();

                var country = _iCountryDal.GetCountryById(countryId);

                Assert.True(country != null);
            }
        }

        /// <summary>
        /// Test database for SaveCountry where saving a new country
        /// </summary>
        /// <param name="name"></param>
        [Theory]
        [InlineData("Sri Lanka")]
        [InlineData("Switzerland")]
        public void SaveCountry_NewCountry_ShouldSaveSuccessfully(string name)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iCountryDal = scope.Resolve<ICountryDal>();

                var country = new Country { Name = name };

                country = _iCountryDal.SaveCountry(country);

                Assert.True(country != null);
            }
        }

        /// <summary>
        /// Test database for SaveCountry method where deleting an existing country
        /// </summary>
        /// <param name="name"></param>
        [Theory]
        [InlineData("Sri Lanka")]
        [InlineData("Switzerland")]
        public void SaveCountry_DeleteExistingCountry_ShouldDeleteSuccessfully(string name)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iCountryDal = scope.Resolve<ICountryDal>();

                var country = _iCountryDal.GetCountryByName(name);

                country = _iCountryDal.SaveCountry(country, true);

                Assert.True(country != null);
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
