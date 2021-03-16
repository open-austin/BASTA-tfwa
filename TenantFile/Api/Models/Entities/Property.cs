using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TenantFile.Api.Common;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Entities
{
    public class Property : IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; } = null!;

        public ICollection<Residence> Residences { get; set; } = new List<Residence>();
        //public virtual Complex Complex { get; set; } = null!;
    }
}