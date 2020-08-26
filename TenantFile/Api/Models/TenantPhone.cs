using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TenantFile.Api.Models
{
    public class TenantPhone
    {
        public int TenantId { get; set; }
        public int PhoneId { get; set; }
        public Phone Phone { get; set; } = null!;


        public virtual Tenant Tenant { get; set; } = null!;

    }
}