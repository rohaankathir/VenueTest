using System.Collections.Generic;

namespace Core.Framework.Contracts
{
    /// <summary>
    /// Interface for the current system user context
    /// </summary>
    public interface ICurrentContext
    {
        #region Properties

        /// <summary>
        /// The unique identifier of the current system user
        /// </summary>
        string CurrentUserId { get; }

        /// <summary>
        /// The full name of the current user
        /// </summary>
        string CurrentUserFullName { get; }

        /// <summary>
        /// The unique identifier of the user's country
        /// </summary>
        int CountryID { get; }

        /// <summary>
        /// The name of the user's country
        /// </summary>
        string CountryName { get; }

        /// <summary>
        /// The name of the user's country
        /// </summary>
        string CountryShortCode { get; }

        /// <summary>
        /// The access groups that the user belongs to
        /// </summary>
        List<string> FunctionGroups { get; }

        bool ImpersonationStatus { get; }

        void SetImpersonatedUser(string userId);

        #endregion
        #region Methods

        /// <summary>
        /// Set the information regarding the country that the user is from
        /// </summary>
        /// <param name="countryId">The unique identifier for the country</param>
        /// <param name="countryName">The name given to the country</param>
        /// <param name="countryShortCode">The short code given to the country</param>
        void SetCountry(int countryId, string countryName, string countryShortCode);

        void ClearImpersonation();

        void RefreshPermission();

        #endregion
    }
}
