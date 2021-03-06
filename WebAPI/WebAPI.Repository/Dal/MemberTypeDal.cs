﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace WebAPI.Repository.Dal
{
    public class MemberTypeDal: IDisposable, IMemberTypeDal
    {
        #region Declarations

        private readonly VenueEntities VenueDataEntities;

        #endregion

        #region Constructor
        public MemberTypeDal()
        {
            VenueDataEntities = new VenueEntities();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fetch All Member Types from Database
        /// </summary>
        /// <returns>List of MemberType objects</returns>
        public List<MemberType> GetMemberTypes()
        {
            return VenueDataEntities.MemberTypes.ToList();
        }

        /// <summary>
        /// Select MemberType by MemberType Id
        /// </summary>
        /// <param name="memberTypeId">int object</param>
        /// <returns>MemberType Object</returns>
        public MemberType GetMemberTypeById(int memberTypeId)
        {
            return VenueDataEntities.MemberTypes.FirstOrDefault(x => x.MemberTypeId == memberTypeId);
        }

        /// <summary>
        /// Save/ Update MemberType and return MemberType object back
        /// </summary>
        /// <param name="memberType">MemberType object to be saved</param>
        /// <returns>MemberType object</returns>
        public MemberType SaveMemberType(MemberType memberType)
        {
            VenueDataEntities.MemberTypes.AddOrUpdate(memberType);
            VenueDataEntities.SaveChanges();

            return VenueDataEntities.MemberTypes.FirstOrDefault(x => x.MemberTypeId == memberType.MemberTypeId);
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
    /// Interface of MemberTypeDal
    /// </summary>
    public interface IMemberTypeDal
    {
        /// <summary>
        /// Fetch All Member Types from Database
        /// </summary>
        /// <returns>List of MemberType objects</returns>
        List<MemberType> GetMemberTypes();

        /// <summary>
        /// Select MemberType by MemberType Id
        /// </summary>
        /// <param name="memberTypeId">int object</param>
        /// <returns>MemberType Object</returns>
        MemberType GetMemberTypeById(int memberTypeId);

        /// <summary>
        /// Save/ Update MemberType and return MemberType object back
        /// </summary>
        /// <param name="memberType">MemberType object to be saved</param>
        /// <returns>MemberType object</returns>
        MemberType SaveMemberType(MemberType memberType);
    }
}