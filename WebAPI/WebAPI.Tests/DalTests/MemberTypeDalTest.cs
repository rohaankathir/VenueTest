using System;
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
    public class MemberTypeDalTest : IClassFixture<BaseTestFixture>, IDisposable
    {
        #region Declarations

        #endregion

        #region Constructor

        #endregion

        #region Unit Tests

        #endregion

        #region Dispose

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
