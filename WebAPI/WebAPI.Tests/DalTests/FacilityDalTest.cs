using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public class FacilityDalTest : IClassFixture<BaseTestFixture>, IDisposable
    {
        #region Declarations

        private IFacilityDal _iFacilityDal;
        private readonly IContainer _iContainer;

        #endregion

        #region Constructor

        public FacilityDalTest(BaseTestFixture fixture)
        {
            _iContainer = fixture.Container;
        }

        #endregion

        #region Unit Tests

        /// <summary>
        /// Check the database select of GetFacilities Method
        /// </summary>
        [Fact]
        public void GetFacilities_GetAll_ShouldReturnFacilities()
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iFacilityDal = scope.Resolve<IFacilityDal>();

                var cityList = _iFacilityDal.GetFacilities();

                Assert.True(cityList.Any());
            }
        }

        /// <summary>
        /// Test the database select of GetFacilityById method
        /// </summary>
        /// <param name="facilityId">int facilityId</param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetFacilityById_ExistingId_ShouldReturnValue(int facilityId)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iFacilityDal = scope.Resolve<IFacilityDal>();

                var facilityResult = _iFacilityDal.GetFacilityById(facilityId);

                Assert.True(facilityResult != null);
            }
        }

        /// <summary>
        /// Test the database select of GetFacilityById where there is no results returned
        /// </summary>
        /// <param name="facilityId">int FacilityId</param>
        [Theory]
        [InlineData(1000000)]
        public void GetFacilityById_NonExistingValue_ShouldNotReturnValue(int facilityId)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iFacilityDal = scope.Resolve<IFacilityDal>();

                var facilityResult = _iFacilityDal.GetFacilityById(facilityId);

                Assert.True(facilityResult == null);
            }
        }

        /// <summary>
        /// Test the database select of GetFacilitiesByVenueId method
        /// </summary>
        /// <param name="venueId">int VenueId</param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetFacilitiesByVenueId_ExistingId_ShouldReturnValue(int venueId)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iFacilityDal = scope.Resolve<IFacilityDal>();

                var facilityResult = _iFacilityDal.GetFacilitiesByVenueId(venueId);

                Assert.True(facilityResult != null);
            }
        }

        /// <summary>
        /// Test the database select of GetFacilityById where there is no results returned
        /// </summary>
        /// <param name="venueId">int VenueId</param>
        [Theory]
        [InlineData(1000000)]
        public void GetFacilitiesByVenueId_NonExistingValue_ShouldNotReturnValue(int venueId)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iFacilityDal = scope.Resolve<IFacilityDal>();

                var facilityResult = _iFacilityDal.GetFacilitiesByVenueId(venueId);

                Assert.True(facilityResult == null);
            }
        }

        /// <summary>
        /// Test database integration of SaveCity method
        /// </summary>
        [Theory, ClassData(typeof(FacilityData))]
        public void SaveCity_SaveNewCity_ShouldSaveSuccessfully(Facility facility)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iFacilityDal = scope.Resolve<IFacilityDal>();

                var cityResult = _iFacilityDal.SaveFacility(facility);

                Assert.True(cityResult != null);
            }
        }

        #endregion

        #region Member Data


        #endregion

        #region Dispose

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    /// <summary>
    /// Class Data - Facility Data
    /// </summary>
    public class FacilityData : IEnumerable<object[]>
    {
        public IEnumerable<object[]> GetFacilityData()
        {
            yield return new object[]
            {
                new Facility
                {
                    VenueId = 1,
                    Name = "Hall 1 Unit Test",
                    Comments = "This is a Hall generated by Unit Test",
                    Height = 25,
                    Length = 20000,
                    Width = 15000,
                    MaxBudget = 350000,
                    MaxPack = 500,
                    MinBudget = 150000,
                    MinPack = 150,
                    FacilityEventStyles = new[]
                    {
                        new FacilityEventStyle() {EventStyleId = 1},
                        new FacilityEventStyle() {EventStyleId = 2}
                    },
                    FacilityEventTypes = new[]
                    {
                        new FacilityEventType() {EventTypeId = 1},
                        new FacilityEventType() {EventTypeId = 2}
                    }
                }
            };
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            return GetFacilityData().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

