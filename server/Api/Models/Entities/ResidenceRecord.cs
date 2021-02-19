using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TenantFile.Api.Common;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Entities
{   
   
    public class ResidenceRecord : IEntity
    {
      
        public int Id { get; set; }
        public DateTime MoveIn { get; set; }
        public DateTime MoveOut { get; set; }

        public virtual Tenant Tenant { get; set; } = null!;
        public virtual Residence Residence { get; set; } = null!;

    }


}