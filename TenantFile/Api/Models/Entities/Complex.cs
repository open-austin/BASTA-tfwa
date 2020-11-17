using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantFile.Api.Models.Entities
{
    /// <summary>
    /// This class is used to represent Properties that are split between multiple addresses
    /// </summary>
    public class Complex //: Premise
    {
        public int Id { get; set; }
        public Address Address { get; set; } = null!;
        public string? Name { get; set; }
        //TODO: Configure this so that getter returns all Residences from all Properties
        //This is done with just a SelectMany
        // public override ICollection<Residence> Residences { get; set; } = null!;
        //public virtual ICollection<Property> Properties { get; set; } = null!;
    }
}
