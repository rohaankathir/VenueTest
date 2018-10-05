using System;
using System.Web.Http;
using Newtonsoft.Json;
using WebAPI.Repository.Dal;

namespace WebAPI.Controllers
{
    public class VenueTypeController : ApiController
    {
        #region Declarations

        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IVenueTypeDal _iVenueTypeDal;

        #endregion

        #region Constructor

        public VenueTypeController(IVenueTypeDal iVenueTypeDal)
        {
            _iVenueTypeDal = iVenueTypeDal;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fetch All Venue Type items from Database
        /// </summary>
        /// <returns>Json object</returns>
        public string GetVenueTypes()
        {
            try
            {
                var venueList = _iVenueTypeDal.GetVenueTypes();
                return JsonConvert.SerializeObject(venueList);
            }
            catch (Exception ex)
            {
                log.Error("Exception in Method GetVenueTypes", ex);
                throw;
            }
        }

        /// <summary>
        /// Select Venue Type by Venue Type Id
        /// </summary>
        /// <param name="venueTypeId">int object</param>
        /// <returns>Json object</returns>
        public string GetVenueTypesById(int venueTypeId)
        {
            try
            {
                var venue = _iVenueTypeDal.GetVenueTypeById(venueTypeId);
                return JsonConvert.SerializeObject(venue);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in Method GetVenueTypesById for id - {venueTypeId}", ex);
                throw;
            }
        }

        /// <summary>
        /// Save/ Update VenueType and return venue type object back
        /// </summary>
        /// <param name="venueTypeJson">Venue Json object to be saved</param>
        /// <returns>VenueType Json object</returns>
        public string SaveVenueType(string venueTypeJson)
        {
            try
            {
                var venueType = JsonConvert.DeserializeObject<VenueType>(venueTypeJson);
                venueType = _iVenueTypeDal.SaveVenueType(venueType);

                return JsonConvert.SerializeObject(venueType);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in Method SaveVenueType for Json Data - {venueTypeJson}", ex);
                throw;
            }
        }

        #endregion
    }
}
