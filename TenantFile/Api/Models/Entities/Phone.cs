using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TenantFile.Api.Common;

namespace TenantFile.Api.Models.Entities
{
    public class Phone : IEntity
    {

        public int Id { get; set; }
        public string PhoneNumber { get; set; } = null!;

        public ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
        public ICollection<Image> Images { get; set; } = new List<Image>();

    }
}