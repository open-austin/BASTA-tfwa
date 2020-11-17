using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantFile.Api.Models.Entities
{
    public abstract class Premise : IPremise
    {
        public int Id { get ; set ; }
        public Address? Address { get; set; } 
        public string? Name { get ; set; }
       // public virtual ICollection<Residence> Residences { get; set; } = null!;
    }
}
