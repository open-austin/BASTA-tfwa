using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantFile.Api.Models
{
    public class ResidenceRecord
    {
        public ResidenceRecord(Property location, string? unitNumber, DateTimeOffset moveIn, DateTimeOffset? moveOut = null)
        {
            Location = location;
            UnitNumber = unitNumber;
            MoveIn = moveIn;
            MoveOut = moveOut;
        }

        public Property Location { get; set; }
        public string? UnitNumber { get; set; }
        public DateTimeOffset MoveIn { get; set; }
        public DateTimeOffset? MoveOut { get; set; }


    }
}
