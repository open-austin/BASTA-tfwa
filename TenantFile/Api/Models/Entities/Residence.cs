using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TenantFile.Api.Models.Entities
{
    public class Residence //: Premise
    {
        
        public int Id { get; set; }
        public string? UnitIdentifier { get; set; } 

        public ICollection<ResidenceRecord> ResidenceRecords { get; set; } = null!;
        public int PropertyId { get; set; }
        //public Property Property { get; set; } = null!;
    }
}