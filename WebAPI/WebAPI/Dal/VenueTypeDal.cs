using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace WebAPI.Dal
{
    public class VenueTypeDal: IDisposable
    {
        #region Declarations

        private readonly VenueDataEntities VenueDataEntities;

        #endregion

        #region Constructor
        public VenueTypeDal()
        {
            VenueDataEntities = new VenueDataEntities();
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
        /// Save/ Update VenueType and return VenueType object back
        /// </summary>
        /// <param name="venueType">VenueType object to be saved</param>
        /// <returns>VenueType object</returns>
        public VenueType SaveVenueType(VenueType venueType)
        {
            VenueDataEntities.VenueTypes.AddOrUpdate(venueType);
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
}