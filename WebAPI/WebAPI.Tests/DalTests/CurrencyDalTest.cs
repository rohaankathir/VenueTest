using System;
using System.Collections.Generic;
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
    public class CurrencyDalTest : IClassFixture<BaseTestFixture>, IDisposable
    {
        #region Declarations

        private ICurrencyDal _iCurrencyDal;
        private readonly IContainer _iContainer;

        #endregion

        #region Constructor

        public CurrencyDalTest(BaseTestFixture fixture)
        {
            _iContainer = fixture.Container;
        }

        #endregion

        #region Unit Tests

        /// <summary>
        /// Check the database select of GetCurrencies Method
        /// </summary>
        [Fact]
        public void GetCurrencies_GetAll_ShouldReturnListOfCurrencies()
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iCurrencyDal = scope.Resolve<ICurrencyDal>();

                var currencyList = _iCurrencyDal.GetCurrencies();

                Assert.True(currencyList.Count > 0);
            }
        }

        /// <summary>
        /// Test the database select of GetCurrencyById method
        /// </summary>
        /// <param name="currencyId">int currencyId</param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetCurrencyById_ExistingId_ShouldReturnValue(int currencyId)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iCurrencyDal = scope.Resolve<ICurrencyDal>();

                var currency = _iCurrencyDal.GetCurrencyById(currencyId);

                Assert.True(currency != null);
            }
        }

        /// <summary>
        /// Test the database select of GetCurrencyById where there is no results returned
        /// </summary>
        /// <param name="currencyId">int CurrencyId</param>
        [Theory]
        [InlineData(1000000)]
        public void GetCurrencyById_NonExistingValue_ShouldNotReturnValue(int currencyId)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iCurrencyDal = scope.Resolve<ICurrencyDal>();

                var currencyList = _iCurrencyDal.GetCurrencyById(currencyId);

                Assert.True(currencyList == null);
            }
        }

        /// <summary>
        /// Test SaveCurrency with database integration
        /// </summary>
        /// <param name="currencyCode"></param>
        /// <param name="symbol"></param>
        /// <param name="userName"></param>
        /// <param name="createdDate"></param>
        [Theory, MemberData(nameof(CurrencyData))]
        public void SaveCurrency_SaveNewCurrency_ShouldSaveSuccessfully(string currencyCode, string symbol, string userName, DateTime createdDate)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iCurrencyDal = scope.Resolve<ICurrencyDal>();

                var currency = new Currency
                {
                    CurrencyCode = currencyCode,
                    Symbol = symbol,
                    CreatedBy = userName,
                    CreatedDate = createdDate
                };

                var currencyResult = _iCurrencyDal.SaveCurrency(currency);

                Assert.True(currencyResult != null);
            }
        }

        /// <summary>
        /// Test database integration of SaveCurrency method with Delete command
        /// </summary>
        /// <param name="currencyCode"></param>
        [Theory, MemberData(nameof(CurrencyDeleteData))]
        public void SaveCurrency_DeleteExistingCurrency_ShouldDeleteSuccessfully(string currencyCode)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iCurrencyDal = scope.Resolve<ICurrencyDal>();

                var currencyResult = _iCurrencyDal.GetCurrencyByCode(currencyCode);

                currencyResult = _iCurrencyDal.SaveCurrency(currencyResult, true);

                Assert.True(currencyResult == null);
            }
        }

        #endregion

        #region Member Data

        /// <summary>
        /// Test Records to save currency
        /// </summary>
        public static IEnumerable<object[]> CurrencyData => new[]
        {
            new object[] { "LKR", "Rs", "rkath", DateTime.Now },
            new object[] { "AUD", "A$", "rkath", DateTime.Now },
            new object[] { "GBP", "£", "rkath", DateTime.Now }
        };

        /// <summary>
        /// Test Records for deleting currency
        /// </summary>
        public static IEnumerable<object[]> CurrencyDeleteData => new[]
        {
            new object[] { "LKR" },
            new object[] { "AUD" },
            new object[] { "GBP" }
        };

        #endregion

        #region Dispose

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
