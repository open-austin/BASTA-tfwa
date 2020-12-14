using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantFile.Api.Common;
using TenantFile.Api.Models.Entities;

namespace TenantFile.Api.Models.Residences
{
    public class CreateResidencePayload : TPayload<Residence>
    {
        public CreateResidencePayload (Residence residence) : base(residence)
        {

        }
        public CreateResidencePayload(UserError error)
            : base(new[] { error })
        {
        }
    }
}