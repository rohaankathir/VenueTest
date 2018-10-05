using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace WebAPI.Repository.Dal
{
    public class CountryDal: IDisposable, ICountryDal
    {
        #region Declarations

        private readonly VenueEntities VenueDataEntities;

        #endregion

        #region Constructor
        public CountryDal()
        {
            VenueDataEntities = new VenueEntities();
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
        /// Select Country by Country name
        /// </summary>
        /// <param name="name">string object</param>
        /// <returns>Country Object</returns>
        public Country GetCountryByName(string name)
        {
            return VenueDataEntities.Countries.FirstOrDefault(x => x.Name == name);
        }

        /// <summary>
        /// Save/ Update/ Delete Country and return Country object back
        /// </summary>
        /// <param name="country">Country object to be saved</param>
        /// <param name="isDelete">Flag to indicate to delete the record from database</param>
        /// <returns>Country object</returns>
        public Country SaveCountry(Country country, bool isDelete = false)
        {
            if (isDelete)
            {
                VenueDataEntities.Countries.Remove(country);
            }
            else
            {
                VenueDataEntities.Countries.AddOrUpdate(country);
            }
            
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

    /// <summary>
    /// Interface of CountryDal
    /// </summary>
    public interface ICountryDal
    {
        /// <summary>
        /// Fetch All Countries from Database
        /// </summary>
        /// <returns>List of Country objects</returns>
        List<Country> GetCountries();

        /// <summary>
        /// Select Country by Country Id
        /// </summary>
        /// <param name="countryId">int object</param>
        /// <returns>Country Object</returns>
        Country GetCountryById(int countryId);

        /// <summary>
        /// Select Country by Country name
        /// </summary>
        /// <param name="name">string object</param>
        /// <returns>Country Object</returns>
        Country GetCountryByName(string name);

        /// <summary>
        /// Save/ Update/ Delete Country and return Country object back
        /// </summary>
        /// <param name="country">Country object to be saved</param>
        /// <param name="isDelete">Flag to indicate to delete the record from database</param>
        /// <returns>Country object</returns>
        Country SaveCountry(Country country, bool isDelete = false);
    }
}