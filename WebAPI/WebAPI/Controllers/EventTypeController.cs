using System;
using System.Web.Http;
using Newtonsoft.Json;
using WebAPI.Repository.Dal;

namespace WebAPI.Controllers
{
    public class EventTypeController : ApiController
    {
        #region Declarations
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IEventTypeDal _iEventTypeDal;
        #endregion

        #region Constructor

        public EventTypeController(IEventTypeDal iEventTypeDal)
        {
            _iEventTypeDal = iEventTypeDal;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Fetch All EventType items from Database
        /// </summary>
        /// <returns>Json object</returns>
        public string GetEventTypes()
        {
            try
            {
                var eventTypeList = _iEventTypeDal.GetEventTypes();
                return JsonConvert.SerializeObject(eventTypeList);
            }
            catch (Exception ex)
            {
                log.Error("Exception in Method GetEventTypes", ex);
                throw;
            }
        }

        /// <summary>
        /// Select EventType by Event Type Id
        /// </summary>
        /// <param name="eventTypeId">int object</param>
        /// <returns>Json object</returns>
        public string GetEventTypeById(int eventTypeId)
        {
            try
            {
                var eventType = _iEventTypeDal.GetEventTypeById(eventTypeId);
                return JsonConvert.SerializeObject(eventType);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in Method GetEventTypeById for id - {eventTypeId}", ex);
                throw;
            }
        }

        /// <summary>
        /// Save/ Update EventType and return EventType object back
        /// </summary>
        /// <param name="eventTypeJson">EventType Json object to be saved</param>
        /// <returns>EventType Json object</returns>
        public string SaveEventType(string eventTypeJson)
        {
            try
            {
                var eventType = JsonConvert.DeserializeObject<EventType>(eventTypeJson);
                eventType = _iEventTypeDal.SaveEventType(eventType);

                return JsonConvert.SerializeObject(eventType);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in Method SaveEventType for Json Data - {eventTypeJson}", ex);
                throw;
            }
        }
        #endregion
    }
}
