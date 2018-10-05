using System;
using System.Web.Http;
using Newtonsoft.Json;
using WebAPI.Repository.Dal;

namespace WebAPI.Controllers
{
    public class CountryController : ApiController
    {
        #region Declarations
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ICountryDal _iCountryDal;
        #endregion

        #region Constructor

        public CountryController(ICountryDal iCountryDal)
        {
            _iCountryDal = iCountryDal;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fetch All Country items from Database
        /// </summary>
        /// <returns>Json object</returns>
        public string GetCountries()
        {
            try
            {
                var countryList = _iCountryDal.GetCountries();
                return JsonConvert.SerializeObject(countryList);
            }
            catch (Exception ex)
            {
                log.Error("Exception in Method GetCountries", ex);
                throw;
            }
        }

        /// <summary>
        /// Select Country by Country Id
        /// </summary>
        /// <param name="countryId">int object</param>
        /// <returns>Json object</returns>
        public string GetCountryById(int countryId)
        {
            try
            {
                var country = _iCountryDal.GetCountryById(countryId);
                return JsonConvert.SerializeObject(country);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in Method GetCountryById for id - {countryId}", ex);
                throw;
            }
        }

        /// <summary>
        /// Save/ Update Country and return Country object back
        /// </summary>
        /// <param name="countryJson">Country Json object to be saved</param>
        /// <returns>Country Json object</returns>
        public string SaveCountry(string countryJson)
        {
            try
            {
                var country = JsonConvert.DeserializeObject<Country>(countryJson);
                country = _iCountryDal.SaveCountry(country);

                return JsonConvert.SerializeObject(country);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in Method SaveCountry for Json Data - {countryJson}", ex);
                throw;
            }
        }

        #endregion
    }
}
