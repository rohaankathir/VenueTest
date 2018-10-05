using System;
using System.Web.Http;
using Newtonsoft.Json;
using WebAPI.Repository.Dal;

namespace WebAPI.Controllers
{
    public class VenueController : ApiController
    {
        #region Declarations

        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IVenueDal _iVenueDal;

        #endregion

        #region Constructor

        public VenueController(IVenueDal iVenueDal)
        {
            _iVenueDal = iVenueDal;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fetch All Venue items from Database
        /// </summary>
        /// <returns>Json object</returns>
        public string GetVenues()
        {
            try
            {
                var venueList = _iVenueDal.GetVenues();
                return JsonConvert.SerializeObject(venueList);
            }
            catch (Exception ex)
            {
                log.Error("Exception in Method GetVenue", ex);
                throw;
            }
        }

        /// <summary>
        /// Select Venue by Venue Id
        /// </summary>
        /// <param name="venueId">int object</param>
        /// <returns>Json object</returns>
        public string GetVenueById(int venueId)
        {
            try
            {
                var venue = _iVenueDal.GetVenueById(venueId);
                return JsonConvert.SerializeObject(venue);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in Method GetVenueById for id - {venueId}", ex);
                throw;
            }
        }

        /// <summary>
        /// Save/ Update Venue and return venue object back
        /// </summary>
        /// <param name="venueJson">Venue Json object to be saved</param>
        /// <returns>Venue Json object</returns>
        public string SaveVenue(string venueJson)
        {
            try
            {
                var venue = JsonConvert.DeserializeObject<Venue>(venueJson);
                venue = _iVenueDal.SaveVenue(venue);

                return JsonConvert.SerializeObject(venue);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in Method SaveVenue for Json Data - {venueJson}", ex);
                throw;
            }
        }

        #endregion
    }
}
