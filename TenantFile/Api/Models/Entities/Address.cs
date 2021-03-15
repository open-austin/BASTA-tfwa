using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using TenantFile.Api.Common;

namespace TenantFile.Api.Models.Entities
{
    public class Address : IEntity
    {
        
        public int Id { get; set; }
             public string Line1 { get; set; } = null!;
             public string? Line2 { get; set; }
        public string? Line3 { get; set; }
        public string? Line4 { get; set; }
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string? ValidationMessage { get; set; }
    }
}