using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using WebAPI.Repository.Dal;

namespace WebAPI.Controllers
{
    public class FacilityController : ApiController
    {
        #region Declarations
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IFacilityDal _iFacilityDal;
        #endregion

        #region Constructor

        public FacilityController(IFacilityDal iFacilityDal)
        {
            _iFacilityDal = iFacilityDal;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Fetch All Facilities from Database
        /// </summary>
        /// <returns>Json object</returns>
        public string GetFacilities()
        {
            try
            {
                var facilityList = _iFacilityDal.GetFacilities();
                return JsonConvert.SerializeObject(facilityList);
            }
            catch (Exception ex)
            {
                log.Error("Exception in Method GetFacilities", ex);
                throw;
            }
        }

        /// <summary>
        /// Select Facility by FacilityId
        /// </summary>
        /// <param name="facilityId">int object</param>
        /// <returns>Json object</returns>
        public string GetFacilityById(int facilityId)
        {
            try
            {
                var facility = _iFacilityDal.GetFacilityById(facilityId);
                return JsonConvert.SerializeObject(facility);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in Method GetEventTypeById for id - {facilityId}", ex);
                throw;
            }
        }

        /// <summary>
        /// Select Facility by VenueId
        /// </summary>
        /// <param name="venueId">int object</param>
        /// <returns>Json object</returns>
        public string GetFacilitiesByVenueId(int venueId)
        {
            try
            {
                var facilityList = _iFacilityDal.GetFacilitiesByVenueId(venueId);
                return JsonConvert.SerializeObject(facilityList);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in Method GetFacilitiesByVenueId for id - {venueId}", ex);
                throw;
            }
        }

        /// <summary>
        /// Save/ Update Facility and return Facility object back
        /// </summary>
        /// <param name="facilityJson">Facility Json object to be saved</param>
        /// <returns>Facility Json object</returns>
        public string SaveFacility(string facilityJson)
        {
            try
            {
                var facility = JsonConvert.DeserializeObject<Facility>(facilityJson);
                facility = _iFacilityDal.SaveFacility(facility);

                return JsonConvert.SerializeObject(facility);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in Method SaveFacility for Json Data - {facilityJson}", ex);
                throw;
            }
        }
        #endregion
    }
}
