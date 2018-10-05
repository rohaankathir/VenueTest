﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace WebAPI.Dal
{
    public class UserGroupDal: IDisposable
    {
        #region Declarations

        private readonly VenueDataEntities VenueDataEntities;

        #endregion

        #region Constructor
        public UserGroupDal()
        {
            VenueDataEntities = new VenueDataEntities();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fetch All UserGroups from Database
        /// </summary>
        /// <returns>List of UserGroup objects</returns>
        public List<UserGroup> GetUserGroups()
        {
            return VenueDataEntities.UserGroups.ToList();
        }

        /// <summary>
        /// Select UserGroup by UserGroup Id
        /// </summary>
        /// <param name="userGroupId">int object</param>
        /// <returns>UserGroup Object</returns>
        public UserGroup GetUserGroupById(int userGroupId)
        {
            return VenueDataEntities.UserGroups.FirstOrDefault(x => x.UserGroupId == userGroupId);
        }

        /// <summary>
        /// Save/ Update UserGroup and return UserGroup object back
        /// </summary>
        /// <param name="userGroup">UserGroup object to be saved</param>
        /// <returns>UserGroup object</returns>
        public UserGroup SaveUserGroup(UserGroup userGroup)
        {
            VenueDataEntities.UserGroups.AddOrUpdate(userGroup);
            VenueDataEntities.SaveChanges();

            return VenueDataEntities.UserGroups.FirstOrDefault(x => x.UserGroupId == userGroup.UserGroupId);
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
}