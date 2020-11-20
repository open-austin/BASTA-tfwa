using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TenantFile.Api.Models.Entities
{
    public class Residence //: Premise
    {
        
        public int Id { get; set; }
        //public string? UnitIdentifier { get; set; }
        
        public int? PropertyId { get; set; }
        public virtual Property? Property { get; set; } 

        public int AddressId { get; set; }
        public virtual Address Address { get; set; } = null!;
        
        public ICollection<TenantEvent> TenantEvents { get; set; } = null!;
    }
}