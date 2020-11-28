using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantFile.Api.Models.Entities
{
    /// <summary>
    /// Replaces ResidenceRecords as a way to track important events for Tenants
    /// </summary>
    public class TenantEvent
    {
        public int Id { get; set; }
        public TenantEventType EventType { get; set; }
        public DateTime TimeOccurred { get; set; }

        public int? TenantId { get; set; } 
        public virtual Tenant? Tenant { get; set; } = null!;
       
        /// <summary>
        /// Using id as the FK and having a Phone as a navigation property allows for the dbContext to track the changes and only call SaveAsync to the db once when a new phone number texts the Twilio endpoint. EF gives a temporary id to the phone that is created in the dbContext that establishes the relationship. Once the db generates the official id, it is assigned properly to the FK PhoneId and the Phone is not needed to complete the reference going foward, allowing it to be marked virtual which means it won't to loaded until it is enumerated
        /// </summary>
        public int? PhoneId { get; set; }
        public virtual Phone? Phone { get; set; }

        // can Residence be infered from other events i.e. move-in when combined with the Tenant of Residence's collection of TenantEvent?. The Residence does not have to be known at the time of the TenantEvent
        //public virtual Residence Residence { get; set; } = null!;


    }
    public enum TenantEventType
    {
        MoveIn,
        MoveOut,
        SentEvidence,
        Eviction,
        CourtDate
    }

}