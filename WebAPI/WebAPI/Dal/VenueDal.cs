using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace WebAPI.Dal
{
    public class VenueDal : IDisposable
    {
        #region Declarations

        private readonly VenueDataEntities VenueDataEntities;

        #endregion

        #region Constructor
        public VenueDal()
        {
            VenueDataEntities = new VenueDataEntities();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fetch All Venue items from Database
        /// </summary>
        /// <returns>List of Venue objects</returns>
        public List<Venue> GetVenues()
        {
            return VenueDataEntities.Venues.ToList();
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
        /// Save/ Update Venue and return venue object back
        /// </summary>
        /// <param name="venue">Venue object to be saved</param>
        /// <returns>Venue object</returns>
        public Venue SaveVenue(Venue venue)
        {
            VenueDataEntities.Venues.AddOrUpdate(venue);
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
}