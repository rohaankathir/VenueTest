using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace WebAPI.Repository.Dal
{
    public class EventStyleDal: IDisposable, IEventStyleDal
    {
        #region Declarations

        private readonly VenueEntities VenueDataEntities;

        #endregion

        #region Constructor
        public EventStyleDal()
        {
            VenueDataEntities = new VenueEntities();
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
        /// Select EventStyle by EventStyle Name
        /// </summary>
        /// <param name="eventStyleName">string object</param>
        /// <returns>EventStyle Object</returns>
        public EventStyle GetEventStyleByName(string eventStyleName)
        {
            return VenueDataEntities.EventStyles.FirstOrDefault(x => x.Name == eventStyleName);
        }

        /// <summary>
        /// Save/ Update EventStyle and return EventStyle object back
        /// </summary>
        /// <param name="eventStyle">EventStyle object to be saved</param>
        /// <param name="isDelete">Flag to indicate to delete the record from database</param>
        /// <returns>EventStyle object</returns>
        public EventStyle SaveEventStyle(EventStyle eventStyle, bool isDelete = false)
        {
            if (isDelete)
            {
                VenueDataEntities.EventStyles.Remove(eventStyle);
            }
            else
            {
                VenueDataEntities.EventStyles.AddOrUpdate(eventStyle);
            }
            
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

    /// <summary>
    /// Interface of EventStyleDal
    /// </summary>
    public interface IEventStyleDal
    {
        /// <summary>
        /// Fetch All Event Styles from Database
        /// </summary>
        /// <returns>List of EventStyle objects</returns>
        List<EventStyle> GetEventStyles();

        /// <summary>
        /// Select EventStyle by EventStyle Id
        /// </summary>
        /// <param name="eventStyleId">int object</param>
        /// <returns>EventStyle Object</returns>
        EventStyle GetEventStyleById(int eventStyleId);

        /// <summary>
        /// Select EventStyle by EventStyle Name
        /// </summary>
        /// <param name="eventStyleName">string object</param>
        /// <returns>EventStyle Object</returns>
        EventStyle GetEventStyleByName(string eventStyleName);

        /// <summary>
        /// Save/ Update EventStyle and return EventStyle object back
        /// </summary>
        /// <param name="eventStyle">EventStyle object to be saved</param>
        /// <param name="isDelete">Flag to indicate to delete the record from database</param>
        /// <returns>EventStyle object</returns>
        EventStyle SaveEventStyle(EventStyle eventStyle, bool isDelete = false);
    }
}