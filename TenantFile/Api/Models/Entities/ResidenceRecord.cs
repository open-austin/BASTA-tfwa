using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TenantFile.Api.Common;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Entities
{   
    /// <summary>
    /// Depricated
    /// This class is trying to capture the state of a Residence at a point in time. In theory, a new Record would be created any time a change occurs in the state of the Residence, e.g. a Tenant moves in or out, but that having a move in and move out makes this class mutible while trying to be immutable and could hold incomplete information about other Tenants/roommates
    /// 
    /// Instead, this can be reprsented as a table of "TenantEvents" or actions where thses objects are appended only
    /// 
    /// Create Enum: MoveIn, MoveOut, SendEvidence, Eviction, 
    /// </summary>
    public class ResidenceRecord
    {
      
        public int Id { get; set; }
        public DateTime MoveIn { get; set; }
        public DateTime MoveOut { get; set; }

        public virtual Tenant Tenant { get; set; } = null!;
        public virtual Residence Residence { get; set; } = null!;

    }


}