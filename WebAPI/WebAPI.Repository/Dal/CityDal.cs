using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace WebAPI.Repository.Dal
{
    public class CityDal: IDisposable, ICityDal
    {
        #region Declarations

        private readonly VenueEntities VenueDataEntities;

        #endregion

        #region Constructor
        public CityDal()
        {
            VenueDataEntities = new VenueEntities();
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
        /// Select City by City name
        /// </summary>
        /// <param name="name">string object</param>
        /// <returns>City Object</returns>
        public City GetCityByName(string name)
        {
            return VenueDataEntities.Cities.FirstOrDefault(x => x.Name == name);
        }

        /// <summary>
        /// Save/ Update/ Delete City and return City object back
        /// </summary>
        /// <param name="city">City object to be saved</param>
        /// <param name="isDelete">Flag to indicate to delete the record from database</param>
        /// <returns>City object</returns>
        public City SaveCity(City city, bool isDelete = false)
        {
            if (isDelete)
            {
                VenueDataEntities.Cities.Remove(city);
            }
            else
            {
                VenueDataEntities.Cities.AddOrUpdate(city);
            }

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

    /// <summary>
    /// Interface of CityDal
    /// </summary>
    public interface ICityDal
    {
        /// <summary>
        /// Fetch All Cities from Database
        /// </summary>
        /// <returns>List of City objects</returns>
        List<City> GetCities();

        /// <summary>
        /// Select City by City Id
        /// </summary>
        /// <param name="cityId">int object</param>
        /// <returns>City Object</returns>
        City GetCityById(int cityId);

        /// <summary>
        /// Select City by City name
        /// </summary>
        /// <param name="name">string object</param>
        /// <returns>City Object</returns>
        City GetCityByName(string name);

        /// <summary>
        /// Save/ Update/ Delete City and return City object back
        /// </summary>
        /// <param name="city">City object to be saved</param>
        /// <param name="isDelete">Flag to indicate to delete the record from database</param>
        /// <returns>City object</returns>
        City SaveCity(City city, bool isDelete = false);
    }
}