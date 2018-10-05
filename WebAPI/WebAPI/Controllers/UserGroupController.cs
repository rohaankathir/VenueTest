using System;
using System.Web.Http;
using Newtonsoft.Json;
using WebAPI.Repository.Dal;

namespace WebAPI.Controllers
{
    public class UserGroupController : ApiController
    {
        #region Declarations
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IUserGroupDal _iUserGroupDal;
        #endregion

        #region Constructor

        public UserGroupController(IUserGroupDal iUserGroupDal)
        {
            _iUserGroupDal = iUserGroupDal;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Fetch All UserGroup items from Database
        /// </summary>
        /// <returns>Json object</returns>
        public string GetUserGroups()
        {
            try
            {
                var userList = _iUserGroupDal.GetUserGroups();
                return JsonConvert.SerializeObject(userList);
            }
            catch (Exception ex)
            {
                log.Error("Exception in Method GetUserGroups", ex);
                throw;
            }
        }

        /// <summary>
        /// Select UserGroup by UserGroupId
        /// </summary>
        /// <param name="userGroupId">int object</param>
        /// <returns>Json object</returns>
        public string GetUserGroupById(int userGroupId)
        {
            try
            {
                var user = _iUserGroupDal.GetUserGroupById(userGroupId);
                return JsonConvert.SerializeObject(user);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in Method GetUserGroupById for id - {userGroupId}", ex);
                throw;
            }
        }

        /// <summary>
        /// Save/ Update UserGroup and return User object back
        /// </summary>
        /// <param name="userGroupJson">UserGroup Json object to be saved</param>
        /// <returns>UserGroup Json object</returns>
        public string SaveUserGroup(string userGroupJson)
        {
            try
            {
                var userGroup = JsonConvert.DeserializeObject<UserGroup>(userGroupJson);
                userGroup = _iUserGroupDal.SaveUserGroup(userGroup);

                return JsonConvert.SerializeObject(userGroup);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in Method SaveUserGroup for Json Data - {userGroupJson}", ex);
                throw;
            }
        }

        #endregion
    }
}
