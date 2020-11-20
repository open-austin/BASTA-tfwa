using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TenantFile.Api.Models.Entities
{
    public class Tenant
    {
        
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int ResidenceId { get; set; }
        public Residence? CurrentResidence { get; set; }//

        public virtual ICollection<TenantEvent> TenantEvents{ get; set; } = null!;
        public virtual ICollection<Phone> Phones { get; set; } = null!;
    }
}