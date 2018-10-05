using System;
using System.Web.Http;
using Newtonsoft.Json;
using WebAPI.Repository.Dal;

namespace WebAPI.Controllers
{
    public class EventStyleController : ApiController
    {
        #region Declarations
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IEventStyleDal _iEventStyleDal;
        #endregion

        #region Constructor

        public EventStyleController(IEventStyleDal iEventStyleDal)
        {
            _iEventStyleDal = iEventStyleDal;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fetch All EventStyles items from Database
        /// </summary>
        /// <returns>Json object</returns>
        public string GetEventStyles()
        {
            try
            {
                var eventStylesList = _iEventStyleDal.GetEventStyles();
                return JsonConvert.SerializeObject(eventStylesList);
            }
            catch (Exception ex)
            {
                log.Error("Exception in Method GetEventStyles", ex);
                throw;
            }
        }

        /// <summary>
        /// Select EventStyle by EventStyle Id
        /// </summary>
        /// <param name="eventStyleId">int object</param>
        /// <returns>Json object</returns>
        public string GetEventStyleById(int eventStyleId)
        {
            try
            {
                var eventStyle = _iEventStyleDal.GetEventStyleById(eventStyleId);
                return JsonConvert.SerializeObject(eventStyle);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in Method GetEventStyleById for id - {eventStyleId}", ex);
                throw;
            }
        }

        /// <summary>
        /// Save/ Update EventStyle and return EventStyle object back
        /// </summary>
        /// <param name="eventStyleJson">EventStyle Json object to be saved</param>
        /// <returns>EventStyle Json object</returns>
        public string SaveEventStyle(string eventStyleJson)
        {
            try
            {
                var eventStyle = JsonConvert.DeserializeObject<EventStyle>(eventStyleJson);
                eventStyle = _iEventStyleDal.SaveEventStyle(eventStyle);

                return JsonConvert.SerializeObject(eventStyle);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in Method SaveEventStyle for Json Data - {eventStyleJson}", ex);
                throw;
            }
        }

        #endregion
    }
}
