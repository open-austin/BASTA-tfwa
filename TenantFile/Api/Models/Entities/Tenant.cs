using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TenantFile.Api.Common;

namespace TenantFile.Api.Models.Entities
{
    public class Tenant : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int? ResidenceId { get; set; }
        public Residence? CurrentResidence { get; set; }

        public ICollection<Phone> Phones { get; set; } = new List<Phone>();
    }
}