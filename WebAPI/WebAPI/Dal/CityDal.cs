using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using WebAPI.Models;

namespace WebAPI.Dal
{
    public class CityDal: IDisposable
    {
        #region Declarations

        private readonly VenueDataEntities VenueDataEntities;

        #endregion

        #region Constructor
        public CityDal()
        {
            VenueDataEntities = new VenueDataEntities();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fetch All Cities from Database
        /// </summary>
        /// <returns>List of City objects</returns>
        public List<City> GetCities()
        {
            return VenueDataEntities.Cities.ToList();
        }

        /// <summary>
        /// Select City by City Id
        /// </summary>
        /// <param name="cityId">int object</param>
        /// <returns>City Object</returns>
        public City GetCityById(int cityId)
        {
            return VenueDataEntities.Cities.FirstOrDefault(x => x.CityId == cityId);
        }

        /// <summary>
        /// Save/ Update City and return City object back
        /// </summary>
        /// <param name="city">City object to be saved</param>
        /// <returns>City object</returns>
        public City SaveCity(City city)
        {
            VenueDataEntities.Cities.AddOrUpdate(city);
            VenueDataEntities.SaveChanges();

            return VenueDataEntities.Cities.FirstOrDefault(x => x.CityId == city.CityId);
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