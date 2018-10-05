using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace WebAPI.Repository.Dal
{
    public class VenueDal : IDisposable, IVenueDal
    {
        #region Declarations

        private readonly VenueEntities VenueDataEntities;

        #endregion

        #region Constructor
        public VenueDal()
        {
            VenueDataEntities = new VenueEntities();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fetch All Venue items from Database
        /// </summary>
        /// <returns>IEnumerable Venue objects</returns>
        public IEnumerable<Venue> GetVenues()
        {
            return VenueDataEntities.Venues;
        }

        /// <summary>
        /// Select Venue by Venue Id
        /// </summary>
        /// <param name="venueId">int object</param>
        /// <returns>Venue Object</returns>
        public Venue GetVenueById(int venueId)
        {
            return VenueDataEntities.Venues.FirstOrDefault(x => x.VenueId == venueId);
        }

        /// <summary>
        /// Select Venue by Name
        /// </summary>
        /// <param name="name">string object</param>
        /// <returns>Venue Object</returns>
        public Venue GetVenueByName(string name)
        {
            return VenueDataEntities.Venues.FirstOrDefault(x => x.Name == name);
        }

        /// <summary>
        /// Save/ Update/ Delete Venue and return venue object back
        /// </summary>
        /// <param name="venue">Venue object to be saved</param>
        /// <param name="isDelete">Flag to indicate to delete the record from database</param>
        /// <returns>Venue object</returns>
        public Venue SaveVenue(Venue venue, bool isDelete = false)
        {
            if (isDelete)
            {
                VenueDataEntities.Venues.Remove(venue);
            }
            else
            {
                VenueDataEntities.Venues.AddOrUpdate(venue);
            }
            
            VenueDataEntities.SaveChanges();

            return VenueDataEntities.Venues.FirstOrDefault(x => x.VenueId == venue.VenueId);
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
    /// Interface of VenueDal
    /// </summary>
    public interface IVenueDal
    {
        /// <summary>
        /// Fetch All Venue items from Database
        /// </summary>
        /// <returns>IEnumerable Venue objects</returns>
        IEnumerable<Venue> GetVenues();

        /// <summary>
        /// Select Venue by Venue Id
        /// </summary>
        /// <param name="venueId">int object</param>
        /// <returns>Venue Object</returns>
        Venue GetVenueById(int venueId);

        /// <summary>
        /// Select Venue by Name
        /// </summary>
        /// <param name="name">string object</param>
        /// <returns>Venue Object</returns>
        Venue GetVenueByName(string name);

        /// <summary>
        /// Save/ Update/ Delete Venue and return venue object back
        /// </summary>
        /// <param name="venue">Venue object to be saved</param>
        /// <param name="isDelete">Flag to indicate to delete the record from database</param>
        /// <returns>Venue object</returns>
        Venue SaveVenue(Venue venue, bool isDelete = false);
    }
}