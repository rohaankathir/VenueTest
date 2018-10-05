using System;
using System.Web.Http;
using Newtonsoft.Json;
using WebAPI.Repository.Dal;

namespace WebAPI.Controllers
{
    public class MemberTypeController : ApiController
    {
        #region Declarations
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IMemberTypeDal _iMemberTypeDal;
        #endregion

        #region Constructor

        public MemberTypeController(IMemberTypeDal iMemberTypeDal)
        {
            _iMemberTypeDal = iMemberTypeDal;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Fetch All MemberType items from Database
        /// </summary>
        /// <returns>Json object</returns>
        public string GetMemberTypes()
        {
            try
            {
                var memberTypeList = _iMemberTypeDal.GetMemberTypes();
                return JsonConvert.SerializeObject(memberTypeList);
            }
            catch (Exception ex)
            {
                log.Error("Exception in Method GetMemberTypes", ex);
                throw;
            }
        }

        /// <summary>
        /// Select MemberType by MemberTypeId
        /// </summary>
        /// <param name="memberTypeId">int object</param>
        /// <returns>Json object</returns>
        public string GetMemberTypeById(int memberTypeId)
        {
            try
            {
                var memberType = _iMemberTypeDal.GetMemberTypeById(memberTypeId);
                return JsonConvert.SerializeObject(memberType);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in Method GetMemberTypeById for id - {memberTypeId}", ex);
                throw;
            }
        }

        /// <summary>
        /// Save/ Update MemberType and return MemberType object back
        /// </summary>
        /// <param name="memberTypeJson">MemberType Json object to be saved</param>
        /// <returns>MemberType Json object</returns>
        public string SaveMemberType(string memberTypeJson)
        {
            try
            {
                var memberType = JsonConvert.DeserializeObject<MemberType>(memberTypeJson);
                memberType = _iMemberTypeDal.SaveMemberType(memberType);

                return JsonConvert.SerializeObject(memberType);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in Method SaveMemberType for Json Data - {memberTypeJson}", ex);
                throw;
            }
        }
        #endregion
    }
}
