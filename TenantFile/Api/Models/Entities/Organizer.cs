using System;
using System.Collections.Generic;

namespace TenantFile.Api.Models
{
    public class Organizer
    {
        
        public string Uid { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<Phone> Phones{ get; set; } = null!;
        public virtual ICollection<Property> Properties { get; set; } = null!;

    }
}