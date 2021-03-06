using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TenantFile.Api.Common;

namespace TenantFile.Api.Models.Entities
{
    public class Residence : IEntity
    {
        public int Id { get; set; }
             
        public int? PropertyId { get; set; }
        public Property? Property { get; set; } 

        public int AddressId { get; set; }
        public Address Address { get; set; } = null!;
    }
}