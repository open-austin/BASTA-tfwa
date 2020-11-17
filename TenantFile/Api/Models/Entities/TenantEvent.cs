using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantFile.Api.Models.Entities
{
    public class TenantEvent
    {
        public int id { get; set; }
        public TenantEventType EventType { get; set; }
        public DateTime Occured { get; set; }
        public virtual Tenant Tenant { get; set; } = null!;
        public virtual Residence Residence { get; set; } = null!;


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