using System.Collections.Generic;
using TenantFile.Api.Common;

namespace TenantFile.Api.Models.Entities
{
    public class Phone : IEntity
    {

        public int Id { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public PreferredLanuage? PreferredLanuage { get; set; }
        public ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
        public ICollection<Image> Images { get; set; } = new List<Image>();

    }
}