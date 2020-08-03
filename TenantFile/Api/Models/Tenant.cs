using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TenantFile.Api.Models
{
    public class Tenant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = null!;


        public virtual ICollection<ResidenceRecord> ResidenceRecords { get; set; } = null!;
        public virtual ICollection<Image> Images { get; set; } = null!;
        public virtual ICollection<TenantPhone> TenantPhones { get; set; } = null!;
    }
}