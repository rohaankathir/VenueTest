using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace WebAPI.Dal
{
    public class CountryDal: IDisposable
    {
        #region Declarations

        private readonly VenueDataEntities VenueDataEntities;

        #endregion

        #region Constructor
        public CountryDal()
        {
            VenueDataEntities = new VenueDataEntities();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fetch All Countries from Database
        /// </summary>
        /// <returns>List of Country objects</returns>
        public List<Country> GetCountries()
        {
            return VenueDataEntities.Countries.ToList();
        }

        /// <summary>
        /// Select Country by Country Id
        /// </summary>
        /// <param name="countryId">int object</param>
        /// <returns>Country Object</returns>
        public Country GetCountryById(int countryId)
        {
            return VenueDataEntities.Countries.FirstOrDefault(x => x.CountryId == countryId);
        }

        /// <summary>
        /// Save/ Update Country and return Country object back
        /// </summary>
        /// <param name="country">Country object to be saved</param>
        /// <returns>Country object</returns>
        public Country SaveCountry(Country country)
        {
            VenueDataEntities.Countries.AddOrUpdate(country);
            VenueDataEntities.SaveChanges();

            return VenueDataEntities.Countries.FirstOrDefault(x => x.CountryId == country.CountryId);
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