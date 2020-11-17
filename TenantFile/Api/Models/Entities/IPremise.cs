using System.Collections.Generic;

namespace TenantFile.Api.Models.Entities
{
    public interface IPremise
    {
        public int Id { get; set; }
        public Address? Address { get; set; }
        public string? Name { get; set; }
        //public abstract ICollection<Residence> Residences { get; set; }

    }
}