﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace WebAPI.Repository.Dal
{
    public class FacilityDal: IDisposable, IFacilityDal
    {
        #region Declarations

        private readonly VenueEntities VenueDataEntities;

        #endregion

        #region Constructor
        public FacilityDal()
        {
            VenueDataEntities = new VenueEntities();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fetch All the Facilities from Database
        /// Do not return list for large set of data
        /// </summary>
        /// <returns>IQueriable Facility</returns>
        public IQueryable<Facility> GetFacilities()
        {
            return VenueDataEntities.Facilities;
        }

        /// <summary>
        /// Select Facility by Facility Id
        /// </summary>
        /// <param name="facilityId">int object</param>
        /// <returns>Facility Object</returns>
        public Facility GetFacilityById(int facilityId)
        {
            return VenueDataEntities.Facilities.FirstOrDefault(x => x.FacilityId == facilityId);
        }

        /// <summary>
        /// Select Facility by Venue Id
        /// </summary>
        /// <param name="venueId">int object</param>
        /// <returns>List of Facility Objects</returns>
        public List<Facility> GetFacilitiesByVenueId(int venueId)
        {
            return VenueDataEntities.Facilities.Where(x => x.VenueId == venueId).ToList();
        }

        /// <summary>
        /// Save/ Update Facility and return Facility object back
        /// </summary>
        /// <param name="facility">Facility object to be saved</param>
        /// <returns>Facility object</returns>
        public Facility SaveFacility(Facility facility)
        {
            VenueDataEntities.Facilities.AddOrUpdate(facility);
            VenueDataEntities.SaveChanges();

            return VenueDataEntities.Facilities.FirstOrDefault(x => x.FacilityId == facility.FacilityId);
        }

        /// <summary>
        /// Save/ Update FacilityImage and return FacilityImage object back
        /// </summary>
        /// <param name="facilityImage">FacilityImage object to be saved</param>
        /// <returns>FacilityImage Id</returns>
        public int SaveFacilityImage(FacilityImage facilityImage)
        {
            VenueDataEntities.FacilityImages.AddOrUpdate(facilityImage);
            VenueDataEntities.SaveChanges();

            return facilityImage.FacilityImageId;
        }

        /// <summary>
        /// Save/ Update FacilityVideo and return FacilityVideo object back
        /// </summary>
        /// <param name="facilityVideo">FacilityVideo object to be saved</param>
        /// <returns>FacilityVideo Id object</returns>
        public int SaveFacilityVideo(FacilityVideo facilityVideo)
        {
            VenueDataEntities.FacilityVideos.AddOrUpdate(facilityVideo);
            VenueDataEntities.SaveChanges();

            return facilityVideo.FacilityVideoId;
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
    /// Interface of FacilityDal
    /// </summary>
    public interface IFacilityDal
    {
        /// <summary>
        /// Fetch All the Facilities from Database
        /// Do not return list for large set of data
        /// </summary>
        /// <returns>IQueriable Facility</returns>
        IQueryable<Facility> GetFacilities();

        /// <summary>
        /// Select Facility by Facility Id
        /// </summary>
        /// <param name="facilityId">int object</param>
        /// <returns>Facility Object</returns>
        Facility GetFacilityById(int facilityId);

        /// <summary>
        /// Select Facility by Venue Id
        /// </summary>
        /// <param name="venueId">int object</param>
        /// <returns>List of Facility Objects</returns>
        List<Facility> GetFacilitiesByVenueId(int venueId);

        /// <summary>
        /// Save/ Update Facility and return Facility object back
        /// </summary>
        /// <param name="facility">Facility object to be saved</param>
        /// <returns>Facility object</returns>
        Facility SaveFacility(Facility facility);

        /// <summary>
        /// Save/ Update FacilityImage and return FacilityImage object back
        /// </summary>
        /// <param name="facilityImage">FacilityImage object to be saved</param>
        /// <returns>FacilityImage Id</returns>
        int SaveFacilityImage(FacilityImage facilityImage);

        /// <summary>
        /// Save/ Update FacilityVideo and return FacilityVideo object back
        /// </summary>
        /// <param name="facilityVideo">FacilityVideo object to be saved</param>
        /// <returns>FacilityVideo Id object</returns>
        int SaveFacilityVideo(FacilityVideo facilityVideo);
    }
}