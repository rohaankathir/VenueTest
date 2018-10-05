using System;
using System.Web.Http;
using Newtonsoft.Json;
using WebAPI.Repository.Dal;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {
        #region Declarations
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IUserDal _iUserDal;
        #endregion

        #region Constructor

        public UserController(IUserDal iUserDal)
        {
            _iUserDal = iUserDal;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Fetch All User items from Database
        /// </summary>
        /// <returns>Json object</returns>
        public string GetUsers()
        {
            try
            {
                var userList = _iUserDal.GetUsers();
                return JsonConvert.SerializeObject(userList);
            }
            catch (Exception ex)
            {
                log.Error("Exception in Method GetUsers", ex);
                throw;
            }
        }

        /// <summary>
        /// Select User by UserId
        /// </summary>
        /// <param name="userId">int object</param>
        /// <returns>Json object</returns>
        public string GetUserById(int userId)
        {
            try
            {
                var user = _iUserDal.GetUserById(userId);
                return JsonConvert.SerializeObject(user);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in Method GetUserById for id - {userId}", ex);
                throw;
            }
        }

        /// <summary>
        /// Save/ Update User and return User object back
        /// </summary>
        /// <param name="userJson">User Json object to be saved</param>
        /// <returns>User Json object</returns>
        public string SaveUser(string userJson)
        {
            try
            {
                var user = JsonConvert.DeserializeObject<User>(userJson);
                user = _iUserDal.SaveUser(user);

                return JsonConvert.SerializeObject(user);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in Method SaveUser for Json Data - {userJson}", ex);
                throw;
            }
        }

        #endregion
    }
}
