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
    public class UserDalTest : IClassFixture<BaseTestFixture>, IDisposable
    {
        #region Declarations

        private IUserDal _iUserDal;
        private readonly IContainer _iContainer;

        #endregion

        #region Constructor

        public UserDalTest(BaseTestFixture fixture)
        {
            _iContainer = fixture.Container;
        }

        #endregion

        #region Unit Tests

        /// <summary>
        /// Check the database select of GetUsers Method
        /// </summary>
        [Fact]
        public void GetUsers_GetAll_ShouldReturnListOfUsers()
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iUserDal = scope.Resolve<IUserDal>();

                var userList = _iUserDal.GetUsers();

                Assert.True(userList.Count > 0);
            }
        }

        /// <summary>
        /// Test the database select of GetUserById method
        /// </summary>
        /// <param name="userId">int UserId</param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetUserById_ExistingId_ShouldReturnValue(int userId)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iUserDal = scope.Resolve<IUserDal>();

                var user = _iUserDal.GetUserById(userId);

                Assert.True(user != null);
            }
        }

        /// <summary>
        /// Test the database select of GetUserById where there is no results returned
        /// </summary>
        /// <param name="userId">int UserId</param>
        [Theory]
        [InlineData(1000000)]
        public void GetUserById_NonExistingValue_ShouldNotReturnValue(int userId)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iUserDal = scope.Resolve<IUserDal>();

                var user = _iUserDal.GetUserById(userId);

                Assert.True(user == null);
            }
        }

        /// <summary>
        /// Test the SaveUser member with database integration
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <param name="address1"></param>
        /// <param name="address2"></param>
        /// <param name="cityId"></param>
        /// <param name="countryId"></param>
        /// <param name="mobileNumber"></param>
        /// <param name="companyName"></param>
        /// <param name="contactPerson"></param>
        /// <param name="memberTypeId"></param>
        /// <param name="userAccessLevelId"></param>
        /// <param name="isActive"></param>
        /// <param name="website"></param>
        [Theory, MemberData(nameof(UserData))]
        public void SaveUser_SaveNewUser_ShouldSaveSuccessfully(string firstName, string lastName, string userName, string email, string address1, string address2, 
            int cityId, int countryId, string mobileNumber, string companyName, string contactPerson, int memberTypeId, int userAccessLevelId, 
            bool isActive, string website)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iUserDal = scope.Resolve<IUserDal>();

                var user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    UserName = userName,
                    Email = email,
                    Address1 = address1,
                    Address2 = address2,
                    CityId = cityId,
                    CountryId = countryId,
                    MobileNumber = mobileNumber,
                    CompanyName = companyName,
                    ContactPerson = contactPerson,
                    MemberTypeId = memberTypeId,
                    UserAccessLevelId = userAccessLevelId,
                    IsActive = isActive,
                    Website = website
                };

                var userResult = _iUserDal.SaveUser(user);

                Assert.True(userResult != null);
            }
        }

        /// <summary>
        /// Test database integration of SaveUser method with Delete command
        /// </summary>
        /// <param name="userName"></param>
        [Theory, MemberData(nameof(UserDeleteData))]
        public void SaveUser_DeleteExistingUser_ShouldDeleteSuccessfully(string userName)
        {
            using (var scope = _iContainer.BeginLifetimeScope(AppContextType.UnitTest.ToString()))
            {
                _iUserDal = scope.Resolve<IUserDal>();

                var userResult = _iUserDal.GetUserByUserName(userName);

                userResult = _iUserDal.SaveUser(userResult, true);

                Assert.True(userResult == null);
            }
        }

        #endregion

        #region Member Data

        /// <summary>
        /// Test Records for Saving Users into database
        /// Records should be in same order as they are accessed
        /// </summary>
        public static IEnumerable<object[]> UserData => new[]
        {
            new object[]
            {
                "FirstName_Rohaan", "LastName_Kathirgamathamby", "UserName_rkath", "Email_rkath@outlook.com", "Address1_Unit1, Henley Road",
                "Address2_Homebush", 1, 61, "0455240904", "Company_Demo", "ContactPerson_Rohaan", 1, 1, true, "www.website1.com"
            },
            new object[]
            {
                "FirstName_Priya", "LastName_Rohaan", "UserName_PriyaR", "Email_priya@outlook.com", "Address1_Unit1, Henley Road",
                "Address2_Homebush", 1, 61, "0455240902", "Company_Demo", "ContactPerson_Priya", 1, 1, true, "www.website2.com"
            }
        };

        /// <summary>
        /// Test Records for Deleting users from database
        /// </summary>
        public static IEnumerable<object[]> UserDeleteData => new[]
        {
            new object[] { "UserName_rkath" },
            new object[] { "UserName_PriyaR" }
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
