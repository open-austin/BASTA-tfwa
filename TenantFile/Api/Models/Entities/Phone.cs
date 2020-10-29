using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TenantFile.Api.Models
{
    public class Phone
    {
        
        public int Id { get; set; }
        public string PhoneNumber { get; set; } = null!;


        public virtual ICollection<TenantPhone> TenantPhones { get; set; } = null!;
        public virtual ICollection<OrganizerPhone> OrganizerPhones { get; set; } = null!;
        public virtual ICollection<Image> Images { get; set; } = null!;

    }
}