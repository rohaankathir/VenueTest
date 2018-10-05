using System;
using System.Web.Http;
using Newtonsoft.Json;
using WebAPI.Repository.Dal;

namespace WebAPI.Controllers
{
    public class CurrencyController : ApiController
    {
        #region Declarations

        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ICurrencyDal _iCurrencyDal;

        #endregion

        #region Constructor

        public CurrencyController(ICurrencyDal iCurrencyDal)
        {
            _iCurrencyDal = iCurrencyDal;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fetch All Currency items from Database
        /// </summary>
        /// <returns>Json object</returns>
        public string GetCurrencies()
        {
            try
            {
                var currencyList = _iCurrencyDal.GetCurrencies();
                return JsonConvert.SerializeObject(currencyList);
            }
            catch (Exception ex)
            {
                log.Error("Exception in Method GetCurrencies", ex);
                throw;
            }
        }

        /// <summary>
        /// Select Currency by currency Id
        /// </summary>
        /// <param name="currencyId">int object</param>
        /// <returns>Json object</returns>
        public string GetCurrencyById(int currencyId)
        {
            try
            {
                var venue = _iCurrencyDal.GetCurrencyById(currencyId);
                return JsonConvert.SerializeObject(venue);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in Method GetCurrencyById for id - {currencyId}", ex);
                throw;
            }
        }

        /// <summary>
        /// Save/ Update Currency and return Currency object back
        /// </summary>
        /// <param name="currencyJson">Currency Json object to be saved</param>
        /// <returns>Currency Json object</returns>
        public string SaveCurrency(string currencyJson)
        {
            try
            {
                var currency = JsonConvert.DeserializeObject<Currency>(currencyJson);
                currency = _iCurrencyDal.SaveCurrency(currency);

                return JsonConvert.SerializeObject(currency);
            }
            catch (Exception ex)
            {
                log.Error($"Exception in Method SaveCurrency for Json Data - {currencyJson}", ex);
                throw;
            }
        }

        #endregion
    }
}
