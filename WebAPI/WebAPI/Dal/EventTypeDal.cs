using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace WebAPI.Dal
{
    public class EventTypeDal: IDisposable
    {
        #region Declarations

        private readonly VenueDataEntities VenueDataEntities;

        #endregion

        #region Constructor
        public EventTypeDal()
        {
            VenueDataEntities = new VenueDataEntities();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fetch All Event Types from Database
        /// </summary>
        /// <returns>List of EventTypes objects</returns>
        public List<EventType> GetEventTypes()
        {
            return VenueDataEntities.EventTypes.ToList();
        }

        /// <summary>
        /// Select EventType by EventType Id
        /// </summary>
        /// <param name="eventTypeId">int object</param>
        /// <returns>EventType Object</returns>
        public EventType GetEventTypeById(int eventTypeId)
        {
            return VenueDataEntities.EventTypes.FirstOrDefault(x => x.EventTypeId == eventTypeId);
        }

        /// <summary>
        /// Save/ Update EventType and return EventType object back
        /// </summary>
        /// <param name="eventType">EventType object to be saved</param>
        /// <returns>EventType object</returns>
        public EventType SaveEventType(EventType eventType)
        {
            VenueDataEntities.EventTypes.AddOrUpdate(eventType);
            VenueDataEntities.SaveChanges();

            return VenueDataEntities.EventTypes.FirstOrDefault(x => x.EventTypeId == eventType.EventTypeId);
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