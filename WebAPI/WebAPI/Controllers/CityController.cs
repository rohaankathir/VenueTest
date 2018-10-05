using System;
using System.Web.Http;
using Newtonsoft.Json;
using WebAPI.Repository.Dal;

namespace WebAPI.Controllers
{
    [RoutePrefix("api")]
    public class CityController : ApiController
    {
        #region Declarations
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ICityDal _iCityDal;
        #endregion

        #region Constructor

        public CityController(ICityDal iCityDal)
        {
            _iCityDal = iCityDal;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fetch All City items from Database
        /// </summary>
        /// <returns>Json object</returns>
        [Route("GetCities")]
        [HttpGet]
        public string GetCities()
        {
            try
            {
                var cityList = _iCityDal.GetCities();
                return JsonConvert.SerializeObject(cityList);
            }
            catch (Exception ex)
            {
                log.Error("Exception in Method GetCities", ex);
                throw;
            }
        }

        /// <summary>
        /// Select City by City Id
        /// </summary>
        /// <param name="cityId">int object</param>
        /// <returns>Json object</returns>
        [Route("GetCityById")]
        [HttpGet]
        public string GetCityById(int cityId)
        {
            try
            {
                var city = _iCityDal.GetCityById(cityId);
                return JsonConvert.SerializeObject(city);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in Method GetCityById for id - {cityId}", ex);
                throw;
            }
        }

        /// <summary>
        /// Save/ Update City and return City object back
        /// </summary>
        /// <param name="cityJson">City Json object to be saved</param>
        /// <returns>City Json object</returns>
        [Route("SaveCity")]
        [HttpPost]
        public string SaveCity(string cityJson)
        {
            try
            {
                var city = JsonConvert.DeserializeObject<City>(cityJson);
                city = _iCityDal.SaveCity(city);

                return JsonConvert.SerializeObject(city);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in Method SaveCity for Json Data - {cityJson}", ex);
                throw;
            }
        }

        #endregion
    }
}
