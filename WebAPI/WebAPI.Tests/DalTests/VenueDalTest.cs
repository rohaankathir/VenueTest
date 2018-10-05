
using System;
using System.Linq;
using Autofac;
using Core.Framework;
using WebAPI.Repository.Dal;
using Xunit;
using System.Collections.Generic;

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
    public class VenueDalTest : IClassFixture<BaseTestFixture>, IDisposable
    {
        #region Declarations

        private IVenueDal _iVenueDal;
        private readonly IContainer _iContainer;

        #endregion

        #region Constructor

        public VenueDalTest(BaseTestFixture fixture)
        {
            _iContainer = fixture.Container;
        }

        #endregion

        #region Unit Tests

        /// <summary>
        /// Check the database select of GetVenues Method
        /// </summary>
        [Fact]
        public void GetVenues_GetAll_ShouldReturnListOfCities()
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iVenueDal = scope.Resolve<IVenueDal>();

                var venueList = _iVenueDal.GetVenues();

                Assert.True(venueList.Any());
            }
        }

        /// <summary>
        /// Test the database select of GetVenueById method
        /// </summary>
        /// <param name="venueId">int VenueId</param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetVenueById_ExistingId_ShouldReturnValue(int venueId)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iVenueDal = scope.Resolve<IVenueDal>();

                var venueResult = _iVenueDal.GetVenueById(venueId);

                Assert.True(venueResult != null);
            }
        }

        /// <summary>
        /// Test the database select of GetVenueById where there is no results returned
        /// </summary>
        /// <param name="venueId">int VenueId</param>
        [Theory]
        [InlineData(1000000)]
        public void GetVenueById_NonExistingValue_ShouldNotReturnValue(int venueId)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iVenueDal = scope.Resolve<IVenueDal>();

                var venueResult = _iVenueDal.GetVenueById(venueId);

                Assert.True(venueResult == null);
            }
        }

        /// <summary>
        /// Test database integration of SaveVenue method
        /// </summary>
        /// <param name="name"></param>
        /// <param name="venueTypeId"></param>
        /// <param name="address1"></param>
        /// <param name="address2"></param>
        /// <param name="countryid"></param>
        /// <param name="cityId"></param>
        [Theory, MemberData(nameof(VenueData))]
        public void SaveVenue_SaveNewVenue_ShouldSaveSuccessfully(string name, int venueTypeId, string address1, string address2,
            int countryid, int cityId)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iVenueDal = scope.Resolve<IVenueDal>();

                var venue = new Venue
                {
                    Name = name,
                    VenueTypeId = venueTypeId,
                    Address1 = address1,
                    Address2 = address2,
                    CountryId = countryid,
                    CityId = cityId
                };

                var venueResult = _iVenueDal.SaveVenue(venue);

                Assert.True(venueResult != null);
            }
        }

        /// <summary>
        /// Test database integration of SaveVenue method with Delete command
        /// </summary>
        /// <param name="Name"></param>
        [Theory]
        [InlineData("Global Towers 1")]
        [InlineData("Global Towers 2")]
        [InlineData("Global Towers 3")]
        public void SaveVenue_DeleteExistingVenue_ShouldDeleteSuccessfully(string Name)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iVenueDal = scope.Resolve<IVenueDal>();

                var venueResult = _iVenueDal.GetVenueByName(Name);

                venueResult = _iVenueDal.SaveVenue(venueResult, true);

                Assert.True(venueResult == null);
            }
        }

        #endregion

        #region Member Data

        /// <summary>
        /// Test Records to save Venue
        /// </summary>
        public static IEnumerable<object[]> VenueData => new[]
        {
            new object[] { "Global Towers 1", 1, "Marine Drive", "Address 2", 94, 1 },
            new object[] { "Global Towers 2", 1, "Marine Drive", "Address 2", 94, 1 },
            new object[] { "Global Towers 3", 1, "Marine Drive", "Address 2", 94, 1 },
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
