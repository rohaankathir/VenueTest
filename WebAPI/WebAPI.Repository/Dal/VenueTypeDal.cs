using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace WebAPI.Repository.Dal
{
    public class VenueTypeDal: IDisposable, IVenueTypeDal
    {
        #region Declarations

        private readonly VenueEntities VenueDataEntities;

        #endregion

        #region Constructor
        public VenueTypeDal()
        {
            VenueDataEntities = new VenueEntities();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fetch All Venue Type items from Database
        /// </summary>
        /// <returns>List of Venue Type objects</returns>
        public List<VenueType> GetVenueTypes()
        {
            return VenueDataEntities.VenueTypes.ToList();
        }

        /// <summary>
        /// Select Venue Type by VenueType Id
        /// </summary>
        /// <param name="venueTypeId">int object</param>
        /// <returns>VenueType Object</returns>
        public VenueType GetVenueTypeById(int venueTypeId)
        {
            return VenueDataEntities.VenueTypes.FirstOrDefault(x => x.VenueTypeId == venueTypeId);
        }

        /// <summary>
        /// Select Venue Type by Name
        /// </summary>
        /// <param name="name">string object</param>
        /// <returns>VenueType Object</returns>
        public VenueType GetVenueTypeByName(string name)
        {
            return VenueDataEntities.VenueTypes.FirstOrDefault(x => x.Name == name);
        }

        /// <summary>
        /// Save/ Update VenueType and return VenueType object back
        /// </summary>
        /// <param name="venueType">VenueType object to be saved</param>
        /// <param name="isDelete">Flag to indicate to delete the record from database</param>
        /// <returns>VenueType object</returns>
        public VenueType SaveVenueType(VenueType venueType, bool isDelete = false)
        {
            if (isDelete)
            {
                VenueDataEntities.VenueTypes.Remove(venueType);
            }
            else
            {
                VenueDataEntities.VenueTypes.AddOrUpdate(venueType);
            }

            VenueDataEntities.SaveChanges();

            return VenueDataEntities.VenueTypes.FirstOrDefault(x => x.VenueTypeId == venueType.VenueTypeId);
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
    /// Interface of VenueTypeDal
    /// </summary>
    public interface IVenueTypeDal
    {
        /// <summary>
        /// Fetch All Venue Type items from Database
        /// </summary>
        /// <returns>List of Venue Type objects</returns>
        List<VenueType> GetVenueTypes();

        /// <summary>
        /// Select Venue Type by VenueType Id
        /// </summary>
        /// <param name="venueTypeId">int object</param>
        /// <returns>VenueType Object</returns>
        VenueType GetVenueTypeById(int venueTypeId);

        /// <summary>
        /// Select Venue Type by Name
        /// </summary>
        /// <param name="name">string object</param>
        /// <returns>VenueType Object</returns>
        VenueType GetVenueTypeByName(string name);

        /// <summary>
        /// Save/ Update VenueType and return VenueType object back
        /// </summary>
        /// <param name="venueType">VenueType object to be saved</param>
        /// <param name="isDelete">Flag to indicate to delete the record from database</param>
        /// <returns>VenueType object</returns>
        VenueType SaveVenueType(VenueType venueType, bool isDelete = false);
    }
}