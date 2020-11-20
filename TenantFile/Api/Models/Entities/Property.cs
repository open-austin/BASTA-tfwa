using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Entities    
{
    public class Property //: Premise
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        //this is already in Residence
        //public string? UnitIdentifier { get; set; }
        public int AddressId { get; set; } 
        public virtual Address Address { get; set; } = null!;

        public virtual ICollection<Residence> Residences { get; set; } = null!;
        public virtual Complex Complex { get; set; } = null!;
    }
}