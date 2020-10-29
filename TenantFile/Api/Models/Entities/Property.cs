using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TenantFile.Api.Models
{
    public class Property
    {      
        public int Id { get; set; }
        public string? Name { get; set; }
        //this is already in Residence
        //public string? UnitIdentifier { get; set; } 
        public Address Address { get; set; } = null!;

        public virtual ICollection<Residence> Residences { get; set; } = null!;
    }
}