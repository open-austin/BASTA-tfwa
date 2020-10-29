using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TenantFile.Api.Models
{
    public class Residence
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //this could be null if there is no apt number, etc.
        public string? UnitIdentifier { get; set; } 


        public virtual ICollection<ResidenceRecord> ResidenceRecords { get; set; } = null!;
        public virtual Property Property { get; set; } = null!;
    }
}