using Autofac;
using System;
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
    public class EventTypeDalTest : IClassFixture<BaseTestFixture>, IDisposable
    {
        #region Declarations

        private IEventTypeDal _iEventTypeDal;
        private readonly IContainer _iContainer;

        #endregion

        #region Constructor

        public EventTypeDalTest(BaseTestFixture fixture)
        {
            _iContainer = fixture.Container;
        }

        #endregion

        #region Unit Tests

        /// <summary>
        /// Check the database select of GetEventTypes Method
        /// </summary>
        [Fact]
        public void GetEventTypes_GetAll_ShouldReturnListOfEventTypes()
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iEventTypeDal = scope.Resolve<IEventTypeDal>();

                var eventTypeList = _iEventTypeDal.GetEventTypes();

                Assert.True(eventTypeList.Count > 0);
            }
        }

        /// <summary>
        /// Test the database select of GetEventTypeById method
        /// </summary>
        /// <param name="eventTypeId">int EventTypeId</param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetEventTypeById_ExistingId_ShouldReturnValue(int eventTypeId)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iEventTypeDal = scope.Resolve<IEventTypeDal>();

                var eventType = _iEventTypeDal.GetEventTypeById(eventTypeId);

                Assert.True(eventType != null);
            }
        }

        /// <summary>
        /// Test the database select of GetEventTypeById where there is no results returned
        /// </summary>
        /// <param name="eventTypeId">int EventTypeId</param>
        [Theory]
        [InlineData(1000000)]
        public void GetEventTypeById_NonExistingValue_ShouldNotReturnValue(int eventTypeId)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iEventTypeDal = scope.Resolve<IEventTypeDal>();

                var eventType = _iEventTypeDal.GetEventTypeById(eventTypeId);

                Assert.True(eventType == null);
            }
        }

        /// <summary>
        /// Test database integration of SaveEventType method
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        [Theory]
        [InlineData("Weddings", "Wedding Unit Test")]
        [InlineData("Bugs Party", "Party Unit Test Personal")]
        public void SaveEventType_SaveNewEventType_ShouldSaveSuccessfully(string name, string description)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iEventTypeDal = scope.Resolve<IEventTypeDal>();

                var eventType = new EventType { Name = name, Description = description };

                var eventTypeResults = _iEventTypeDal.SaveEventType(eventType);

                Assert.True(eventTypeResults != null);
            }
        }

        /// <summary>
        /// Test database integration of SaveEventType method with Delete command
        /// </summary>
        /// <param name="Name"></param>
        [Theory]
        [InlineData("Weddings")]
        [InlineData("Bugs Party")]
        public void SaveEventType_DeleteExistingEventType_ShouldDeleteSuccessfully(string Name)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iEventTypeDal = scope.Resolve<IEventTypeDal>();

                var eventTypeResult = _iEventTypeDal.GetEventTypeByName(Name);

                eventTypeResult = _iEventTypeDal.SaveEventType(eventTypeResult, true);

                Assert.True(eventTypeResult == null);
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
