using System;
using System.Collections.Generic;

namespace Core.Framework.Contracts
{
    [Serializable()]
    public class UserProfile
    {
        // Normal User data
        public UserProfile()
        {
            FunctionGroups = new List<string>();
        }

        public string CountryName { get; set; }

        public int CountryID { get; set; }

        public string CountryShortCode { get; set; }

        public string FullName { get; set; }

        public List<string> FunctionGroups { get; set; }

        // Impersonated User data
        public bool IsImperonated { get; set; }

        public string ImpersonatedUserId { get; set; }

        public string ImpersonatedUserName { get; set; }

        public List<string> ImpersonatedUserFunctionGroups { get; set; }
    }
}
