using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantFile.Api.Models.Entities
{
    public class TenantEvent
    {
        public int Id { get; set; }
        public TenantEventType EventType { get; set; }
        public DateTime DateOccurred { get; set; }

        public int? TenantId { get; set; } 
        public int? PhoneId { get; set; } 
        //virtual means LazyLoading and changetracking 
        public virtual Tenant? Tenant { get; set; } = null!;

        //Residence can be infered from other events i.e. move-in. The Residence does not have to be known at the time of the TenantEvent..Phone needs a Collection of Unassigned TenantEvents that can be assigned like 
        //public virtual Residence Residence { get; set; } = null!;


    }
    public enum TenantEventType
    {
        MoveIn,
        MoveOut,
        SendEvidence,
        Eviction,
        CourtDate
    }

}