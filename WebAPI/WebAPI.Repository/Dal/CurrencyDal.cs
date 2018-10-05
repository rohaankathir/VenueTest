using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace WebAPI.Repository.Dal
{
    public class CurrencyDal : IDisposable, ICurrencyDal
    {
        #region Declarations

        private readonly VenueEntities VenueDataEntities;

        #endregion

        #region Constructor
        public CurrencyDal()
        {
            VenueDataEntities = new VenueEntities();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fetch All Currencies
        /// </summary>
        /// <returns>List of Currency objects</returns>
        public List<Currency> GetCurrencies()
        {
            return VenueDataEntities.Currencies.ToList();
        }

        /// <summary>
        /// Fetch Currency by Id
        /// </summary>
        /// <param name="currencyId"></param>
        /// <returns>Currency object</returns>
        public Currency GetCurrencyById(int currencyId)
        {
            return VenueDataEntities.Currencies.FirstOrDefault(x => x.CurrencyId == currencyId);
        }

        /// <summary>
        /// Fetch Currency by Currency Code
        /// </summary>
        /// <param name="currencyCode"></param>
        /// <returns>Currency object</returns>
        public Currency GetCurrencyByCode(string currencyCode)
        {
            return VenueDataEntities.Currencies.FirstOrDefault(x => x.CurrencyCode == currencyCode.Trim());
        }

        /// <summary>
        /// Save/ Update/ Delete Currency and return Currency object back
        /// </summary>
        /// <param name="currency">Currency object to be saved</param>
        /// <param name="isDelete">Flag to indicate to delete the record from database</param>
        /// <returns>Currency object</returns>
        public Currency SaveCurrency(Currency currency, bool isDelete = false)
        {
            if (isDelete)
            {
                VenueDataEntities.Currencies.Remove(currency);
            }
            else
            {
                VenueDataEntities.Currencies.AddOrUpdate(currency);
            }
            
            VenueDataEntities.SaveChanges();

            return VenueDataEntities.Currencies.FirstOrDefault(x => x.CurrencyId == currency.CurrencyId);
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
    /// Interface of CurrencyDal
    /// </summary>
    public interface ICurrencyDal
    {
        /// <summary>
        /// Fetch All Currencies
        /// </summary>
        /// <returns>List of Currency objects</returns>
        List<Currency> GetCurrencies();

        /// <summary>
        /// Fetch Currency by Id
        /// </summary>
        /// <param name="currencyId"></param>
        /// <returns>Currency object</returns>
        Currency GetCurrencyById(int currencyId);

        /// <summary>
        /// Fetch Currency by Currency Code
        /// </summary>
        /// <param name="currencyCode"></param>
        /// <returns>Currency object</returns>
        Currency GetCurrencyByCode(string currencyCode);

        /// <summary>
        /// Save/ Update/ Delete Currency and return Currency object back
        /// </summary>
        /// <param name="currency">Currency object to be saved</param>
        /// <param name="isDelete">Flag to indicate to delete the record from database</param>
        /// <returns>Currency object</returns>
        Currency SaveCurrency(Currency currency, bool isDelete = false);
    }
}