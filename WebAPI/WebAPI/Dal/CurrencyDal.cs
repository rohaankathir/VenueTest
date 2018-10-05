using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Dal
{
    public class CurrencyDal : IDisposable
    {
        #region Declarations

        private readonly VenueDataEntities VenueDataEntities;

        #endregion

        #region Constructor
        public CurrencyDal()
        {
            VenueDataEntities = new VenueDataEntities();
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
}