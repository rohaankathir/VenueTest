//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI.Repository.Dal
{
    using System;
    using System.Collections.Generic;
    
    public partial class FacilityRating
    {
        public int FacilityRatingId { get; set; }
        public int FacilityId { get; set; }
        public byte Rating { get; set; }
    
        public virtual Facility Facility { get; set; }
    }
}
