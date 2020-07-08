using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantFile.Api.Models
{
    public class ResidenceRecord
    {
        public ResidenceRecord(Property residence, string? unitNumber, DateTimeOffset moveIn, DateTimeOffset? moveOut = null)
        {
            Residence = residence;
            UnitNumber = unitNumber;
            MoveIn = moveIn;
            MoveOut = moveOut;
        }

        public Property Residence { get; set; }//More appropriate as DocumentReference?
        public string? UnitNumber { get; set; }
        public DateTimeOffset MoveIn { get; set; }
        public DateTimeOffset? MoveOut { get; set; }


    }
}
