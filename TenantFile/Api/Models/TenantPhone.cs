using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TenantFile.Api.Models
{
    public class TenantPhone
    {
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
        public int PhoneId { get; set; }
        public Phone Phone { get; set; } = null!;
    }
}