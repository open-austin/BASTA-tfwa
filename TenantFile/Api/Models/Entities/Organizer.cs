using System;
using System.Collections.Generic;

namespace TenantFile.Api.Models.Entities
{
    public class Organizer
    {

        public string Uid { get; set; } = null!;
        public string Name { get; set; } = null!;

        public ICollection<Phone> Phones { get; set; } = new List<Phone>();
        public ICollection<Property> Properties { get; set; } = new List<Property>();

    }
}