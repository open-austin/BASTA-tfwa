using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TenantFile.Api.Common;

namespace TenantFile.Api.Models.Entities
{
    public class Phone
    {
        
        public int Id { get; set; }
        public string PhoneNumber { get; set; } = null!;

        public virtual ICollection<Tenant> Tenants { get; set; } = null!;
        public virtual ICollection<Organizer> Organizers { get; set; } = null!;
        public virtual ICollection<Image> Images { get; set; } = null!;

    }
}