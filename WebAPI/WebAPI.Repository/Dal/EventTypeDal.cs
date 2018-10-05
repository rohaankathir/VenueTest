using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace WebAPI.Repository.Dal
{
    public class EventTypeDal: IDisposable, IEventTypeDal
    {
        #region Declarations

        private readonly VenueEntities VenueDataEntities;

        #endregion

        #region Constructor
        public EventTypeDal()
        {
            VenueDataEntities = new VenueEntities();
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
        /// Select EventType by name
        /// </summary>
        /// <param name="name">string object</param>
        /// <returns>EventType Object</returns>
        public EventType GetEventTypeByName(string name)
        {
            return VenueDataEntities.EventTypes.FirstOrDefault(x => x.Name == name);
        }

        /// <summary>
        /// Save/ Update/ Delete EventType and return EventType object back
        /// </summary>
        /// <param name="eventType">EventType object to be saved</param>
        /// <param name="isDelete">Flag to indicate to delete the record from database</param>
        /// <returns>EventType object</returns>
        public EventType SaveEventType(EventType eventType, bool isDelete = false)
        {
            if (isDelete)
            {
                VenueDataEntities.EventTypes.Remove(eventType);
            }
            else
            {
                VenueDataEntities.EventTypes.AddOrUpdate(eventType);
            }
           
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

    /// <summary>
    /// Interface of EventTypeDal
    /// </summary>
    public interface IEventTypeDal
    {
        /// <summary>
        /// Fetch All Event Types from Database
        /// </summary>
        /// <returns>List of EventTypes objects</returns>
        List<EventType> GetEventTypes();

        /// <summary>
        /// Select EventType by EventType Id
        /// </summary>
        /// <param name="eventTypeId">int object</param>
        /// <returns>EventType Object</returns>
        EventType GetEventTypeById(int eventTypeId);

        /// <summary>
        /// Select EventType by name
        /// </summary>
        /// <param name="name">string object</param>
        /// <returns>EventType Object</returns>
        EventType GetEventTypeByName(string name);

        /// <summary>
        /// Save/ Update/ Delete EventType and return EventType object back
        /// </summary>
        /// <param name="eventType">EventType object to be saved</param>
        /// <param name="isDelete">Flag to indicate to delete the record from database</param>
        /// <returns>EventType object</returns>
        EventType SaveEventType(EventType eventType, bool isDelete = false);
    }
}