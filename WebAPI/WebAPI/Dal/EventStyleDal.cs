using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace WebAPI.Dal
{
    public class EventStyleDal: IDisposable
    {
        #region Declarations

        private readonly VenueDataEntities VenueDataEntities;

        #endregion

        #region Constructor
        public EventStyleDal()
        {
            VenueDataEntities = new VenueDataEntities();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fetch All Event Styles from Database
        /// </summary>
        /// <returns>List of EventStyle objects</returns>
        public List<EventStyle> GetEventStyles()
        {
            return VenueDataEntities.EventStyles.ToList();
        }

        /// <summary>
        /// Select EventStyle by EventStyle Id
        /// </summary>
        /// <param name="eventStyleId">int object</param>
        /// <returns>EventStyle Object</returns>
        public EventStyle GetEventStyleById(int eventStyleId)
        {
            return VenueDataEntities.EventStyles.FirstOrDefault(x => x.EventStyleId == eventStyleId);
        }

        /// <summary>
        /// Save/ Update EventStyle and return EventStyle object back
        /// </summary>
        /// <param name="eventStyle">EventStyle object to be saved</param>
        /// <returns>EventStyle object</returns>
        public EventStyle SaveEventStyle(EventStyle eventStyle)
        {
            VenueDataEntities.EventStyles.AddOrUpdate(eventStyle);
            VenueDataEntities.SaveChanges();

            return VenueDataEntities.EventStyles.FirstOrDefault(x => x.EventStyleId == eventStyle.EventStyleId);
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