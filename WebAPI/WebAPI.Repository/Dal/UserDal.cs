using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace WebAPI.Repository.Dal
{
    public class UserDal : IDisposable, IUserDal
    {
        #region Declarations

        private readonly VenueEntities VenueDataEntities;

        #endregion

        #region Constructor
        public UserDal()
        {
            VenueDataEntities = new VenueEntities();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fetch All Users from Database
        /// </summary>
        /// <returns>List of User objects</returns>
        public List<User> GetUsers()
        {
            return VenueDataEntities.Users.ToList();
        }

        /// <summary>
        /// Select User by User Id
        /// </summary>
        /// <param name="userId">int object</param>
        /// <returns>User Object</returns>
        public User GetUserById(int userId)
        {
            return VenueDataEntities.Users.FirstOrDefault(x => x.UserId == userId);
        }

        /// <summary>
        /// Select User by UserName
        /// </summary>
        /// <param name="userName">string object</param>
        /// <returns>User Object</returns>
        public User GetUserByUserName(string userName)
        {
            return VenueDataEntities.Users.FirstOrDefault(x => x.UserName == userName.Trim());
        }

        /// <summary>
        /// Save/ Update User and return User object back
        /// </summary>
        /// <param name="user">User object to be saved</param>
        /// <param name="isDelete">Flag to indicate to delete the record from database</param>
        /// <returns>User object</returns>
        public User SaveUser(User user, bool isDelete = false)
        {
            if (isDelete)
            {
                VenueDataEntities.Users.Remove(user);
            }
            else
            {
                VenueDataEntities.Users.AddOrUpdate(user);
            }
            
            VenueDataEntities.SaveChanges();

            return VenueDataEntities.Users.FirstOrDefault(x => x.UserId == user.UserId);
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Dispose objects
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code is implemented in Dispose(boolean)  
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources  
                VenueDataEntities?.Dispose();
            }
        }

        #endregion
    }

    /// <summary>
    /// Interface of UserGroupDal
    /// </summary>
    public interface IUserDal
    {
        /// <summary>
        /// Fetch All Users from Database
        /// </summary>
        /// <returns>List of User objects</returns>
        List<User> GetUsers();

        /// <summary>
        /// Select User by User Id
        /// </summary>
        /// <param name="userId">int object</param>
        /// <returns>User Object</returns>
        User GetUserById(int userId);

        /// <summary>
        /// Select User by UserName
        /// </summary>
        /// <param name="userName">string object</param>
        /// <returns>User Object</returns>
        User GetUserByUserName(string userName);

        /// <summary>
        /// Save/ Update User and return User object back
        /// </summary>
        /// <param name="user">User object to be saved</param>
        /// <param name="isDelete">Flag to indicate to delete the record from database</param>
        /// <returns>User object</returns>
        User SaveUser(User user, bool isDelete = false);
    }
}
