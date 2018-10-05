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
    public class VenueTypeDalTest : IClassFixture<BaseTestFixture>, IDisposable
    {
        #region Declarations

        private IVenueTypeDal _iVenueTypeDal;
        private readonly IContainer _iContainer;

        #endregion

        #region Constructor

        public VenueTypeDalTest(BaseTestFixture fixture)
        {
            _iContainer = fixture.Container;
        }

        #endregion

        #region Unit Tests

        /// <summary>
        /// Check the database select of GetVenueTypes Method
        /// </summary>
        [Fact]
        public void GetVenueTypes_GetAll_ShouldReturnListOfVenueTypes()
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iVenueTypeDal = scope.Resolve<IVenueTypeDal>();

                var venueTypeList = _iVenueTypeDal.GetVenueTypes();

                Assert.True(venueTypeList.Count > 0);
            }
        }

        /// <summary>
        /// Test the database select of GetVenueTypeById method
        /// </summary>
        /// <param name="venueTypeId">int VenueTypeId</param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetVenueTypeById_ExistingId_ShouldReturnValue(int venueTypeId)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iVenueTypeDal = scope.Resolve<IVenueTypeDal>();

                var venueTypeResult = _iVenueTypeDal.GetVenueTypeById(venueTypeId);

                Assert.True(venueTypeResult != null);
            }
        }

        /// <summary>
        /// Test the database select of GetVenueTypeById where there is no results returned
        /// </summary>
        /// <param name="venueTypeId">int VenueTypeId</param>
        [Theory]
        [InlineData(1000000)]
        public void GetVenueTypeById_NonExistingValue_ShouldNotReturnValue(int venueTypeId)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iVenueTypeDal = scope.Resolve<IVenueTypeDal>();

                var venueTypeResult = _iVenueTypeDal.GetVenueTypeById(venueTypeId);

                Assert.True(venueTypeResult == null);
            }
        }

        /// <summary>
        /// Test database integration of SaveVenueType method
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        [Theory]
        [InlineData("VenueType1", "Venue Type 1 Unit Test")]
        [InlineData("VenueType2", "Venue Type 2 Unit Test")]
        public void SaveVenueType_SaveNewVenueType_ShouldSaveSuccessfully(string name, string description)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iVenueTypeDal = scope.Resolve<IVenueTypeDal>();

                var venueType = new VenueType { Name = name, Description = description };

                var venueTypeResult = _iVenueTypeDal.SaveVenueType(venueType);

                Assert.True(venueTypeResult != null);
            }
        }

        /// <summary>
        /// Test database integration of SaveVenueType method with Delete command
        /// </summary>
        /// <param name="Name"></param>
        [Theory]
        [InlineData("VenueType1")]
        [InlineData("VenueType2")]
        public void SaveVenueType_DeleteExistingVenueType_ShouldDeleteSuccessfully(string Name)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iVenueTypeDal = scope.Resolve<IVenueTypeDal>();

                var venueResult = _iVenueTypeDal.GetVenueTypeByName(Name);

                venueResult = _iVenueTypeDal.SaveVenueType(venueResult, true);

                Assert.True(venueResult == null);
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
