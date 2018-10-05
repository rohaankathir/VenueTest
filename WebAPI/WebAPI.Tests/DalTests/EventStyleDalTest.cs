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
    public class EventStyleDalTest : IClassFixture<BaseTestFixture>, IDisposable
    {
        #region Declarations

        private IEventStyleDal _iEventStyleDal;
        private readonly IContainer _iContainer;

        #endregion

        #region Constructor

        public EventStyleDalTest(BaseTestFixture fixture)
        {
            _iContainer = fixture.Container;
        }

        #endregion

        #region Unit Tests

        /// <summary>
        /// Check the database select of GetEventStyles Method
        /// </summary>
        [Fact]
        public void GetEventStyles_GetAll_ShouldReturnListOfEventStyles()
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iEventStyleDal = scope.Resolve<IEventStyleDal>();

                var eventStyleList = _iEventStyleDal.GetEventStyles();

                Assert.True(eventStyleList.Count > 0);
            }
        }

        /// <summary>
        /// Test the database select of GetEventStyleById method
        /// </summary>
        /// <param name="eventStyleId">int EventStyleId</param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetEventStyleById_ExistingId_ShouldReturnValue(int eventStyleId)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iEventStyleDal = scope.Resolve<IEventStyleDal>();

                var eventStyle = _iEventStyleDal.GetEventStyleById(eventStyleId);

                Assert.True(eventStyle != null);
            }
        }

        /// <summary>
        /// Test the database select of GetEventStyleById where there is no results returned
        /// </summary>
        /// <param name="eventStyleId">int GetEventStyleById</param>
        [Theory]
        [InlineData(1000000)]
        public void GetEventStyleById_NonExistingValue_ShouldNotReturnValue(int eventStyleId)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iEventStyleDal = scope.Resolve<IEventStyleDal>();

                var eventStyleResult = _iEventStyleDal.GetEventStyleById(eventStyleId);

                Assert.True(eventStyleResult == null);
            }
        }

        /// <summary>
        /// Test SaveEventStyle method with database integration
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        [Theory, MemberData(nameof(EventStyleData))]
        public void SaveEventStyle_SaveNewEvent_ShouldSaveSuccessfully(string name, string description)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iEventStyleDal = scope.Resolve<IEventStyleDal>();

                var currency = new EventStyle
                {
                    Name = name,
                    Description = description
                };

                var currencyResult = _iEventStyleDal.SaveEventStyle(currency);

                Assert.True(currencyResult != null);
            }
        }

        /// <summary>
        /// Test database integration of SaveEventStyle method with Delete command
        /// </summary>
        /// <param name="eventStyleName"></param>
        [Theory, MemberData(nameof(EventStyleDeleteData))]
        public void SaveCurrency_DeleteExistingCurrency_ShouldDeleteSuccessfully(string eventStyleName)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iEventStyleDal = scope.Resolve<IEventStyleDal>();

                var eventResult = _iEventStyleDal.GetEventStyleByName(eventStyleName);

                eventResult = _iEventStyleDal.SaveEventStyle(eventResult, true);

                Assert.True(eventResult == null);
            }
        }

        #endregion

        #region Member Data
        /// <summary>
        /// Test data to save new EventStyles
        /// </summary>
        public static IEnumerable<object[]> EventStyleData => new[]
        {
            new object[] { "Open Plan", "Open Space plan" },
            new object[] { "Table Plan", "Table arrangement" },
            new object[] { "Birthday", "Birthday parties" },
            new object[] { "Wedding", "Weddings" }
        };

        /// <summary>
        /// Test data for delete EventStyles
        /// </summary>
        public static IEnumerable<object[]> EventStyleDeleteData => new[]
        {
            new object[] {"Open Plan"},
            new object[] {"Table Plan"},
            new object[] {"Birthday"},
            new object[] {"Wedding"}
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
