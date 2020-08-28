using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TenantFile.Api.Models
{
    public class ResidenceRecord
    {
      
        public int Id { get; set; }
        public DateTimeOffset MoveIn { get; set; }
        public DateTimeOffset MoveOut { get; set; }


        public virtual Tenant Tenant { get; set; } = null!;
        public virtual Residence Residence { get; set; } = null!;

    }
}